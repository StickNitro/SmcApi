# Order Payment Processor

This is a Azure Function used to process and orders payments based on a set of rules. The Azure Function is an HttpTrigger Function but this could also be an EventHubTrigger.

The main logic is in each of the `IPaymentRule` implementations which decide upon what gets added to the payment response, these are evaluated in the `IPaymentProcessorService`.

# Development

## Running the Function

To run the function open Visual Studio and hit `F5`. You will then be able to send `POST` requests to the function at `http://localhost:7071/api/orders/{orderId}/payment` and send the input model in the body, [see here for example](#orderprocessingfunction).

## Testing

To run the XUnit unit tests, open the solution in Visual Studio and select `Test > Run All Tests` or press `Ctrl + R, A`.

# Overview

## IPaymentRule

Each rule is defined in an `IPaymentRule` instance and added as a scoped instance in the `Startup`, this makes the rules definitions easily extensible by defining a new `IPaymentRule` instance and adding that to the `IServiceProvider` on startup. 

The following rules are defined in this library:

### PhysicalProductPaymentRule

This rule generates a `PickingNote` of type `Shipping` in the response, this rule also sets the `GenerateCommissionPayment` to `true` in order to trigger a commission payment to the agent.

### BookProductPaymentRule

This rule is similar to the previous rule but  generates an additional `Royalty` `PickingNote` in the response.

### MembershipPaymentRule or MembershipUpgradePaymentRule

These rules generate a response that will trigger either a new membership or an upgrade to a membership, in addition the `SendNotifition` is set to `true` to trigger an email to the owner and inform them of the activation/upgrade.

### VideoRegulationPaymentRule

This rule will add a 'First Aid' video to the packing slip only if the order type is `Video` and the video name is `Learning to Ski`

## IPaymentProcessorService

The `IPaymentProcessorService` iterates over all defined `IPaymentRules` which are injected into the classes constructor. Each rule is executed by calling the `Process` method and the result is checked, if the result is `null` then the service continues on to the next rule, once a non-null result is received then the service exits the loop and returns.

If the service iterates over all `IPaymentRules` and none of the rules evaluate then a new `PaymentOutputModel` is returned from the service.

## OrderProcessingFunction

The `OrderProcessingFunction` can be called using a `POST` request using Postman. The body will require a `PaymentInputModel`, for example:

```json
{
	"id": "guid",
	"orderId": "guid",
	"type": "ProductType",
	"name": "string"
}
```

`ProductType` is an enumerated type with the values `Physical`, `Book`, `Membership`, `MembershipUpgrade`, `Video`.
