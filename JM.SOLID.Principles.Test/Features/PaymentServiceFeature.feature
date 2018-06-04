Feature: PaymentServiceFeature
	In order to avoid silly mistakes
	As a Payment Service idot
	I want to test all possible scecenarios



Scenario Outline: All the payment method should be run successfully for valid payment schemes
Given I have an account <AccountNumber>,<Balance>,<AccountStatus>,<AllowedPaymentSchemes>
And I have Payment Request details <PaymentScheme>,<CreditorAccountNumber>,<DebtorAccountNumber>,<Amount>,<PaymentDate>
When I make the payment
Then The payment should be successful
Examples: 
 | AllowedPaymentSchemes | AccountNumber | Balance | AccountStatus | PaymentScheme  | CreditorAccountNumber | DebtorAccountNumber | Amount | PaymentDate |
 | FasterPayments        | AC12345       | 100.50  | Live          | FasterPayments | AC1234                | AC12345             | 50.00  | 2018-04-30  |
 | Bacs                  | AC12346       | 55.35   | Live          | Bacs           | AC1234                | AC12345             | 40.00  | 2018-04-30  |
 | Chaps                 | AC12347       | 45.50   | Live          | Chaps          | AC1234                | AC12345             | 50.00  | 2018-04-30  |

Scenario Outline: Invalid debtor Allowed Payment Schemes should fail the payment
Given I have an account <AccountNumber>,<Balance>,<AccountStatus>,<AllowedPaymentSchemes>
And I have Payment Request details <PaymentScheme>,<CreditorAccountNumber>,<DebtorAccountNumber>,<Amount>,<PaymentDate>
When I make the payment
Then The payment should be fail
Examples: 
 | AllowedPaymentSchemes | AccountNumber | Balance | AccountStatus | PaymentScheme  | CreditorAccountNumber | DebtorAccountNumber | Amount | PaymentDate |
 | Chaps                 | AC12345       | 100.50  | Live          | FasterPayments | AC1234                | AC12345             | 50.00  | 2018-04-30  |
 | FasterPayments        | AC12346       | 55.35   | Live          | Bacs           | AC1234                | AC12345             | 40.00  | 2018-04-30  |
 | Bacs                  | AC12347       | 45.50   | Live          | Chaps          | AC1234                | AC12345             | 50.00  | 2018-04-30  |

Scenario Outline: Chap payment type should be failed when the debitor account status is not LIVE
Given I have an account <AccountNumber>,<Balance>,<AccountStatus>,<AllowedPaymentSchemes>
And I have Payment Request details <PaymentScheme>,<CreditorAccountNumber>,<DebtorAccountNumber>,<Amount>,<PaymentDate>
When I make the payment
Then The payment should be fail
Examples: 
 | AllowedPaymentSchemes | AccountNumber | Balance | AccountStatus | PaymentScheme | CreditorAccountNumber | DebtorAccountNumber | Amount | PaymentDate |
 | Chaps                 | AC12345       | 100.50  | Disabled      | Chaps         | AC1234                | AC12345             | 50.00  | 2018-04-30  |

 Scenario Outline: FasterPayments payment type should be failed when the debitor account bance is less than the requested payment amount
Given I have an account <AccountNumber>,<Balance>,<AccountStatus>,<AllowedPaymentSchemes>
And I have Payment Request details <PaymentScheme>,<CreditorAccountNumber>,<DebtorAccountNumber>,<Amount>,<PaymentDate>
When I make the payment
Then The payment should be fail
Examples: 
 | AllowedPaymentSchemes | AccountNumber | Balance | AccountStatus | PaymentScheme  | CreditorAccountNumber | DebtorAccountNumber | Amount | PaymentDate |
 | FasterPayments        | AC12345       | 30.35   | Live          | FasterPayments | AC1234                | AC12345             | 50.00  | 2018-04-30  |