Feature: Login feature with action


    @loginWithAction
    Scenario: login web page action
        Given I navigate to "https://www.demo.guru99.com/V4/"
        And I change the page spec to LoginPageAction
        And I perform login-action action