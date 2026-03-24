# Error Information

## Using HTTP Status Codes (RFC 9110)

When processing an incoming HTTP request, a failure may occur on the server side 
if the server is unable to process the request due to internal errors or infrastructure issues.

Server errors (5xx):

- internal server error (500 Internal Server Error)
- server unavailable (503 Service Unavailable)
- request timeout (504 Gateway Timeout)

Errors may also occur due to an invalid request sent by the client.
Such errors can usually be resolved by modifying the request.

Client errors (4xx):

- bad request (400 Bad Request)
- access denied (403 Forbidden)
- resource not found (404 Not Found)
- request conflicts with the current state of the resource (409 Conflict)

In both cases, the appropriate HTTP status code is selected according to the
[RFC 9110: HTTP Semantics](https://datatracker.ietf.org/doc/html/rfc9110).

## Using Problem Details (RFC 7807 / RFC 9457)

The HTTP status code indicates only the general category of the error
(4xx for client errors, 5xx for server errors).

To provide additional information about the error, the API follows the
Problem Details specification defined in [RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807) and
[RFC 9457](https://datatracker.ietf.org/doc/html/rfc9457).

### Error Response Structure

All API errors are returned using the Problem Details object defined by RFC 7807 / RFC 9457.

The response contains the standard fields defined by the specification and
may include additional fields used by the service for programmatic error handling and diagnostics.

Example response:

```json
{
  "errors": {
    "monthCount": [
      "The maximum length is 64 characters"
    ]
  },
  "type": "https://tools.ietf.org/doc/html/rfc9110#section-15.5.1",
  "title": "The name is too long",
  "status": 400,
  "detail": "The maximum length of the person's name is 64 characters (the current value is 80).",
  "instance": "&timezone=utc&from=1773456656509&to=1773457256509&var-app=MindTrailService&var-traceId=26c5353f046a4e35025d3bdf0f04fd3d",
  "traceId": "26c5353f046a4e35025d3bdf0f04fd3d",
  "errorCode": "mind_trail.person_name_too_long",
  "maxLength": 64
}
```

The response may contain additional fields. Client applications must ignore unknown fields.
Clients should rely only on documented fields when implementing error-handling logic.

### Field title

The `title` field provides a short, human-readable summary of the problem type.

The value of `title` should remain the same for the same type of error (except for localization)
and serves as a human-readable identifier of the problem.

A period must not be placed at the end of the `title` value.

Client applications should not use the `title` value for programmatic error handling
(for example, in `if` or `switch` statements).
Instead, the `errorCode` field must be used because it contains a stable machine-readable error identifier.

Examples of `title` values:

- Invalid month count
- The name is too long
- Duplicate subscription plan

### Field detail

The `detail` field provides a more detailed explanation of the specific occurrence of the problem.

According to RFC 9457 recommendations, if the `detail` field is present,
its content should help the client understand the cause of the error and, if necessary, correct the request.
It should not contain debugging or internal technical information such as stack traces or implementation details.

Unlike `title`, the `detail` field may contain dynamic data related to the specific request.
A period at the end of the `detail` value is allowed.

Examples of `detail` values:

- The month count must be greater than zero
- The maximum size of the subscription plan name is 128 characters (the current value is 209)
- The subscription plan with SKU 'P-1' and name 'Pro' already exists

### Field instance

The `instance` field contains a URI that identifies a specific occurrence of the problem.

The `instance` value can be used in two ways:

1. As a dereferenceable URI that allows the client to retrieve
   additional information about the problem if the URI is accessible.
   The URI may be absolute or relative (interpreted relative to the API base URL).

2. As an opaque identifier used by the server or support team
   to locate information in internal logging or monitoring systems.
   In this case, the value is meaningful to the server but opaque to the client.

The client may provide the `instance` value to the support team to help identify the specific incident.

For example, the `instance` field may contain a link to a monitoring dashboard (such as Grafana)
that includes logs, metrics, and traces related to the incident.

In Dev and QA environments such a link may be a dereferenceable URI,
while in production environments `instance` may contain only an opaque identifier
or a query string used internally to locate relevant monitoring data.

### Field type

The `type` field contains a URI that identifies the category of the error.

According to the Problem Details specification (RFC 7807 / RFC 9457),
the `type` value is typically used as a link to documentation describing the problem type.
This URI may point to a documentation page or another resource providing additional information about the error.

In this API, the `type` field references the corresponding section of the HTTP Semantics specification (RFC 9110)
describing the returned HTTP status code.

For consistency with the default ASP.NET behavior, the links use the `tools.ietf.org` domain used by the framework:

- 400: https://tools.ietf.org/doc/html/rfc9110#section-15.5.1
- 404: https://tools.ietf.org/doc/html/rfc9110#section-15.5.5
- 409: https://tools.ietf.org/doc/html/rfc9110#section-15.5.10

Thus, the `type` field provides a general description of the error category at the HTTP protocol level.

For programmatic error handling, clients should rely on the `errorCode` field,
which provides a stable machine-readable error identifier.

### Field errorCode

The `errorCode` field contains a stable machine-readable identifier of the error.

According to the Problem Details specification (RFC 7807 / RFC 9457),
servers may include additional fields in the error object using the extension mechanism.
The `errorCode` field is such an extension and is intended for programmatic error handling by client applications.

Unlike `title` and `detail`, which are intended for display to the user,
`errorCode` is used by client applications to implement error-handling logic.

The value of `errorCode` must remain stable and should not change between API versions.
Existing error codes should not be removed;
instead, new codes should be added while obsolete ones may be marked as deprecated.

Client applications may use `errorCode`, for example:

- to display localized user messages
- to implement specific error-handling logic in the client application

The format of the `errorCode` value is:

```
<service>.<error_identifier>
```

where:

- `<service>` is the service name
- `<error_identifier>` is a short description of the error in snake_case

Examples of `errorCode` values:

- mind_trail.person_duplicate
- mind_trail.person_name_too_long

### Error Parameters

In some cases, the `detail` message may contain values that describe the problem
(for example, minimum or maximum allowed values).

To allow client applications to use these values programmatically,
they are also included as additional fields in the error object using the Problem Details extension mechanism.
These additional fields are referred to as **error parameters**.

This allows client applications to use parameter values when constructing user messages (including localized messages)
and when displaying constraints in the user interface.

For example, if the error is related to exceeding a limit, the response may look like this:

```json
{
  "type": "https://tools.ietf.org/doc/html/rfc9110#section-15.5.1",
  "title": "The name is too long",
  "status": 400,
  "detail": "The maximum length of the person's name is 64 characters (the current value is 80).",
  "maxLength": 64
}
```

In this example, the `maxValue` parameter appears in the error message and is also provided separately
so that client applications can use it when displaying information to the user.

Error parameters could theoretically be grouped in a separate object (for example, `parameters`).
However, according to RFC 9457, additional fields are added directly to the Problem Details object,
forming a flat response structure.

### Field traceId

The `traceId` field contains the identifier of the request trace.

This field is included using the Problem Details extension mechanism and is used for error diagnostics.

The `traceId` value allows locating records related to the request in logging and distributed tracing systems.

Client applications may provide the `traceId` value to the support team
to help quickly locate information about the request in internal systems.

The service uses distributed tracing based on OpenTelemetry,
so the `traceId` value remains the same throughout the entire lifecycle of the request,
including calls between multiple services.

### Validation Errors

If an error is related to an invalid value of a specific request field,
the response uses the standard ASP.NET `ValidationProblemDetails` mechanism.

In this case, the error object additionally contains the `errors` field,
which is a dictionary with the following structure:

- the key is the name of the invalid request property
- the value is a list of error messages for that property

This allows client applications to associate the error with a specific user interface field.

Example validation error response:

```json
{
  "errors": {
    "monthCount": [
      "The maximum length is 64 characters"
    ]
  },
  "type": "https://tools.ietf.org/doc/html/rfc9110#section-15.5.1",
  "title": "The name is too long",
  "status": 400,
  "detail": "The maximum length of the person's name is 64 characters (the current value is 80)."
}
```

The property name in the `errors` field corresponds to the property name in the JSON request.

The error description in the `errors` field should be concise
and is typically a shortened form of the message provided in the `detail` field.
A period must not be placed at the end of the description.