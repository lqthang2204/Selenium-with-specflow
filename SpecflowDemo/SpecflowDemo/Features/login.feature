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

  
