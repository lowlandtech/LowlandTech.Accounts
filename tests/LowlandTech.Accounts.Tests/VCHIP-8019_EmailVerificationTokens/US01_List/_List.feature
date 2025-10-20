
# --------------------------------------------------------------------
# Feature: EmailVerificationTokens - List and filter
# --------------------------------------------------------------------
@VCHIP-8019 @US01 @Blazor @API @CRUD
Feature: List and filter EmailVerificationTokens
  As a user
  I want to browse EmailVerificationTokens with paging and filters
  So that I can quickly find and manage EmailVerificationTokens

  @SC01
  Scenario: Load first page with default sorting
    Given I open the EmailVerificationTokens page
    When the page loads the first page with default size
    Then it calls GET /api/emailverificationtokens?skip=0&take=100               # UAC001
    And the table renders the returned EmailVerificationTokens                   # UAC002

  @SC02
  Scenario: Search by Id or Name
    Given I am on the EmailVerificationTokens page with data available
    When I enter "search-term" in the search box and apply
    Then it calls GET /api/emailverificationtokens?skip=0&take=100&search=search-term # UAC003
    And only EmailVerificationTokens whose Id or Name contains "search-term" are shown # UAC004

  @SC03
  Scenario: Filter by property
    Given I am on the EmailVerificationTokens page
    When I set a filter value
    Then it calls GET /api/emailverificationtokens with the filter parameter # UAC005
    And only matching EmailVerificationTokens are shown              # UAC006

  @SC04
  Scenario: Change page size above server max clamps to 500
    Given I change page size to 2000
    When the list reloads
    Then it calls GET /api/emailverificationtokens?skip=0&take=500               # UAC007
    And at most 500 items are rendered                             # UAC008
