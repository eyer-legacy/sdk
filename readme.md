# SDK

This SDK is meant to make it easier to use AIMS Platform inbound API, which can be used to push your own data into the platform for us to monitor.

## Overview

First, let us introduce some terminology. When you first register with AIMS, you need to create an _environment_ – a logical container for all the data you're going to have monitored. This _environment_ will be able to aggregate multiple _systems_ that are physically or logically interconnected, and correlate statistics and events between those _systems_.

Each _system_ is fed with data by an _agent_ – an application that this SDK is meant to make easier to create. An _agent_ has a number of data types that it can send to AIMS: the topology of your system (including _nodes_, their _status_, static _properties_, and _links_ between them), as well as _statistics_ and _events_. Each _agent_ has its own types of these data that have to be defined when the _agent_ is registered.

So, here's a brief algorithm of how you can use this SDK:

 1. Register with AIMS.
 2. Create an environment.
 3. Register your new agent.
 4. Download this SDK.
 5. Create an agent application using it (examples coming soon!).
 6. Add a system to your environment to connect this agent to.
 7. Run the agent and have your data in AIMS Platform!

Now we acknowledge that some steps are not as intuitive as we'd like them to be, so we'll provide a more technical explanation for them below.

## Registering your agent

There is an API that is to be used to register a new agent. We'll add support for it in this SDK as well in the coming days, but for now, here's the API address to use: 

    POST https://beta-api.aimsinnovation.com/api/agents

To this address you need to send a JSON object defining our agent. This definition is probably the most complex part of creating your agent. Let's start with an example:

```JSON
{
  "id": "acme.int-sys",
  "name": "ACME Internal System",
  "metadata": {
    "nodeTypes": [ {
        "name": "acme.int-sys.server",
        "displayName": "ACME Server",
        "eventTypes": [ "acme.int-sys.server-error" ],
        "propertyTypes": [ "acme.int-sys.server-os" ],
        "statTypes": [ "acme.int-sys.server-cpu" ],
        "statuses": [ "acme.int-sys.unavailable", "aims.core.stopped", "aims.core.started" ]
    } ],
    "nodePropertyTypes": [ {
        "name": "acme.int-sys.server-os",
        "displayName": "operating system"
    } ],
    "nodeStatuses": [ {
        "name": "acme.int-sys.unavailable",
        "displayName": "unavailable",
        "type": "stopped"
    } ],
    "eventTypes": [ {
        "name": "acme.int-sys.server-error",
        "displayName": "ACME server error"
    } ],
    "statTypes": [ {
        "name": "acme.int-sys.server-cpu",
        "displayName": "CPU load",
        "aggregation": "avg",
        "nodeAggregation": "avg",
        "unitType": "percent"
    } ]
  }
}
```

This example defines an agent called __ACME Internal System__ (this is the a name that should be descriptive of a _kind of system_ monitored by this agent, e.g. "Windows Server", "PostgreSQL", or "Azure Service Bus"). It also has an `id` that has to consist of two parts: `<company-name>.<agent-name>`, with a total length of 5 to 16 characters (which may be Latin letters and hyphens, except for the one dot splitting the company and the agent names).

Then, all the data types that this agent is going to send to AIMS are defined. Here, it contains one node type, one type of property for that node, a custom node status, an event type and a statistic type. Each of these types has a `name` that consists of the agent's `id` followed by a name for the type itself, with a dot in between the parts. It also has a `displayName` which is something you're going to see in the GUI and email notifications/reports when this type is referred to.

Additionally, statistic types have some more fields that define how the statistics are going to be aggregated, and how the metric is going to be called.

`aggregation` and `nodeAggregation` can be one of the following:

 - `avg`
 - `sum`
 - `max`

`unitType` can be one of the following:

 - `quantity`
 - `percent`
 - `milliseconds`
 - `bytes`
 - `hertz`

Status definition also has a `type` which defines how the status will be treated. It can take one of the following values:

 - `running`
 - `paused`
 - `stopped`
 - `undefined`

And perhaps most importantly, node type definition contains references to all the other data types that a node of a certain type can have. The data types referenced must be either defined in the same metadata definition, or, in case of node statuses they can also be one of the predefined values:

 - `aims.core.running`
 - `aims.core.paused`
 - `aims.core.stopped`
 - `aims.core.enabled`
 - `aims.core.disabled`
 - `aims.core.undefined`
