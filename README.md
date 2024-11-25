# Usage
## Get authorizarion token
To get an authorizarion token you need to:
* Using Postman make a Get to http://localhost:5149/api/Authorization
* Pass the form-data with Email and Password 

Use any of the following values:
```json
{ "test1@test.com", "123" }
{ "test2@test.com", "123" }
{ "test3@test.com", "123" }
{ "test4@test.com", "123" }
{ "test5@test.com", "123" }
```

## Card API
Before making any request you need to add the authorizarion Bearer token in Postman.

### Create card
To create a card you need to:
* Using Postman make a Put to http://localhost:5149/api/Card

Using the following body
```json
{
  "number": "string",
  "expiryMonth": "string",
  "expiryYear": "string",
  "cvc": "string",
  "holderName": "string",
  "balance": 0
}
```

* Returns a card id that will be used for Pay or Get card balance.

Lookup for testing cards at https://docs.stripe.com/testing#cards
Note: We are not storing card information, we are tokenizing the cards and storing the token.

### Pay
To make a payment you need to:
* Using Postman make a Post to http://localhost:5149/api/Card

Using the following body
```json
{
  "cardId": 0,
  "amount": 0,
  "description": "string"
}
```

* Return the payment id.

### Get card balance
To get the balance you need to:
* Using Postman make a Get to http://localhost:5149/api/Card/{id}
Replace {id} for the Id of your card.

* Return the balance.