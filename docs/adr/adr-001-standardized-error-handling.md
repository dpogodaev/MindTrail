# ADR-001: Standardized Error Handling Using Problem Details

## Status

Accepted

## Context

Client applications (primarily frontend) require the ability to handle API errors programmatically.

This requirement applies primarily to client errors (4xx responses),
where the client is expected to react to the error and potentially adjust the request.

There are two key requirements:

1. **Localization of error messages**  
   Error messages returned by the API are not suitable for direct display to end users in all cases.  
   Client applications must be able to map errors to localized, user-friendly messages.
  
2. **Providing structured constraint data**  
   Some errors include important contextual values (e.g., minimum/maximum limits).  
   Clients need access to these values to:
    - display accurate validation messages
    - reflect constraints in the user interface (e.g., form limits)

Standard HTTP status codes alone are insufficient, 
as they only describe the general class of the error (e.g., 400, 404, 500) 
and do not provide machine-readable semantics for specific cases.

The Problem Details specification (RFC 7807 / RFC 9457) defines a standard structure for error responses 
but does not prescribe application-specific error identification or structured constraint handling.

## Decision

We standardize API error responses using the Problem Details format (RFC 7807 / RFC 9457)
with the following extensions:

### 1. Application-specific error code

Each error includes an additional field `errorCode`:

- Provides a stable, machine-readable identifier of the error
- Used by client applications for programmatic handling
- Remains stable across API versions
- Not removed once introduced (deprecated instead if needed)

Client applications must use `errorCode` instead of `title` or `detail` for:

- localization of error messages
- implementing conditional logic (e.g., `if`, `switch`)

### 2. Error parameters

When an error contains dynamic constraint values (e.g., limits),
they are included as additional fields in the response.

Example:

```json
{
  "errorCode": "mind_trail.person_name_too_long",
  "detail": "The maximum length of the person's name is 64 characters (the current value is 80).",
  "maxLength": 64
}
```

Characteristics:

- Parameters are included as top-level fields in the Problem Details object
- Follow a flat structure, in accordance with RFC 9457
- Intended for programmatic use by client applications
- May also be referenced in the `detail` message

### 3. Separation of concerns

The error response is structured to clearly separate responsibilities:

| Field       | Purpose                           |
|-------------|-----------------------------------|
| `status`    | HTTP-level classification         |
| `type`      | Reference to HTTP semantics       |
| `title`     | Human-readable error summary      |
| `detail`    | Detailed, contextual message      |
| `errorCode` | Machine-readable error identifier |
| parameters  | Structured constraint data        |
| `traceId`   | Diagnostics and observability     |

### 4. Constraints on field usage

- `title` and `detail` are intended for display purposes only
- `errorCode` is the only supported field for programmatic error handling
- Clients must ignore any unknown fields
- Clients should rely only on documented fields

### 5. Scope of application

These extensions are primarily intended for 4xx responses, where client-side handling is required.

## Consequences

### Positive

- Enables reliable localization of error messages on the client side
- Allows clients to implement robust, maintainable error-handling logic
- Provides structured access to constraint values (e.g., limits)
- Aligns with RFC 7807 / RFC 9457 and ASP.NET conventions
- Clean separation between human-readable and machine-readable data

### Negative

- Requires maintaining a stable registry of `errorCode` values
- Introduces additional design responsibility when defining new errors 
  (each case will require its own domain exception)
- Slight increase in response size due to additional fields

### Neutral

- Clients must implement mapping from `errorCode` to user-facing messages
- Some duplication may occur between `detail` and parameter fields

## Alternatives Considered

### 1. Using `title` or `detail` for programmatic handling

Rejected because:

- values are not stable (may change due to wording or localization)
- not suitable for machine processing
- tightly couples client logic to server message text

### 2. Embedding parameters inside `detail` only

Rejected because:

- requires parsing text on the client side
- not reliable or maintainable
- prevents structured access to constraint values

### 3. Grouping parameters in a nested object (e.g., `parameters`)

Rejected because:

- contradicts the RFC 9457 recommendation for a flat structure
- adds unnecessary nesting without clear benefit

### 4. Custom error response format (not using Problem Details)

Rejected because:

- duplicates an existing standard
- reduces interoperability
- increases maintenance cost

## References

- Returning informative API Errors
  https://www.speakeasy.com/api-design/errors
- RFC 9110: HTTP Semantics  
  https://datatracker.ietf.org/doc/html/rfc9110
- RFC 7807: Problem Details for HTTP APIs  
  https://datatracker.ietf.org/doc/html/rfc7807
- RFC 9457: Problem Details for HTTP APIs (revision)  
  https://datatracker.ietf.org/doc/html/rfc9457
