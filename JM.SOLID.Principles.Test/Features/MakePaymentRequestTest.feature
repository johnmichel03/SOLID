Feature: MakePaymentRequestTest
	In order to avoid silly mistakes
	As a MakePaymentRequestTest idiot
	I want to be validated all the paramters


Scenario Outline: Should validation be successful for valid MakePaymentRequest parameters 
	Given I have an payment request information <CreditorAccountNumber>,<DebtorAccountNumber>,<Amount>,<PaymentDate>,<PaymentScheme>
	When I call the MakePaymentRequest validation
	Then The validation should be successful
Examples: 
 | CreditorAccountNumber | DebtorAccountNumber | Amount | PaymentDate | PaymentScheme  |
 | AC1234                | AC12345             | 50.00  | 2018-04-30  | FasterPayments |
 | AC1234                | AC12345             | 40.00  | 2018-04-30  | Bacs           |
 | AC1234                | AC12345             | 50.00  | 2018-04-30  | Chaps          |

 Scenario Outline: Should validation be fail for invalid MakePaymentRequest parameters 
	Given I have an payment request information <CreditorAccountNumber>,<DebtorAccountNumber>,<Amount>,<PaymentDate>,<PaymentScheme>
	When I call the MakePaymentRequest validation
	Then The validation should be fail
Examples: 
 | CreditorAccountNumber | DebtorAccountNumber | Amount | PaymentDate | PaymentScheme  |
 | AC1234                | AC1234              | 50.00  | 2018-04-30  | FasterPayments |
 | AC1234                | AC1235              | 0.00   | 2018-04-30  | FasterPayments |
 |                       | AC1235              | 50.00  | 1800-04-30  | FasterPayments |