Feature: Login feature with action overide value


    @loginWithAction
    Scenario: login web page action
        Given I navigate to "https://www.demo.guru99.com/V4/"
        And I change the page spec to LoginPageActionWithOverride
        And I perform login-action action with override values
        | Field      | Value      |
        | user-field | mngr535424 |
        | pass-field | ynadAhY    |