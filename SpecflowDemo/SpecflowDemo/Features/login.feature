Feature: Login feature


@login
Scenario: login web page
   Given I navigate to "https://www.demo.guru99.com/V4/"
   And I change the page spec to LoginPage
   And I type "mngr535424" into element user-field
   And I type "ynadAhY" into element pass-field   
   And I click element login-button
   And I change the page spec to IndexPage
    And I verify the text for element title-header is "Guru99 Bank"
   And I click element edit-customer
   And I wait for element new-customer to be ENABLED
   And I click element new-customer
   And I navigate to "https://www.google.com/"
   And I change the page spec to googlePage
   And I wait for element search-field to be DISPLAYED
   And I type "automation" into element search-field
   And I wait 5 seconds
   
    @new-customer
    Scenario: new custmer
        Given I navigate to "https://www.demo.guru99.com/V4/"
        And I change the page spec to LoginPage
        And I change the page spec to LoginPageAction
        And I perform login-action action
        And I change the page spec to IndexPage
        And I verify the text for element title-header is "Guru99 Bank"
        And I wait for element new-customer to be ENABLED
        And I click element new-customer
        And I change the page spec to AddCustomerPage
        And I perform add-customer-action action with override values
        | Field          |    Value      |
        | customer-name  |    customer   |
        | dob-field      |    02022000   |
        | address-field  |    address    |
        | city-field     |    city       |
        | state-field    |    state      |
        | pin-field      |    pin        |
        | phone-field    |    phone      |
        | email-field    |    email      |
        | password-field |    password   |
        And I wait 50 seconds
        


  
