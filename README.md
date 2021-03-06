# promises-base

Certain service-oriented architecture (SOA) patterns use the metaphor of the railway to describe a scenario where a service responds to requests using a common endpoint.

In traditional SOA, the services are built as discrete endpoints.  It is the application's responsibility to figure our which endpoint will deliver the response desired and which type of request must be used to deliver this response.

In a railway pattern, requests are targeted at a common "rail-line" (single endpoint) and travel across the line until they reach the end.  The line connects the request to its corresponding "promise" (rail car) which carries it across the line through switch points.  These switch points come in three categories:

- Validators - which determine whether the request (or any other environment data pertaining to that request) is valid
- AuthChallengers - which determine whether the line is authorized to provide a response to that request
- Executors - which perform tasks using the request (or other environment data pertaining to that request) to transform that request into a response

This project aims to create a generic implementation of the promise that follows SOLID principles and fluent design for use in a railway service.

## NuGet feed
These packages are published to the NuGet feed: https://www.myget.org/F/termine/