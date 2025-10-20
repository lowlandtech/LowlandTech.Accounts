
# --------------------------------------------------------------------
# Feature: Sessions - List and filter
# --------------------------------------------------------------------
@VCHIP-8018 @US01 @Blazor @API @CRUD
Feature: List and filter Sessions
  As a user
  I want to browse Sessions with paging and filters
  So that I can quickly find and manage Sessions

  @SC01
  Scenario: Load first page with default sorting
    Given I open the Sessions page
    When the page loads the first page with default size
    Then it calls GET /api/sessions?skip=0&take=100               # UAC001
    And the table renders the returned Sessions                   # UAC002

  @SC02
  Scenario: Search by Id or Name
    Given I am on the Sessions page with data available
    When I enter "search-term" in the search box and apply
    Then it calls GET /api/sessions?skip=0&take=100&search=search-term # UAC003
    And only Sessions whose Id or Name contains "search-term" are shown # UAC004

  @SC03
  Scenario: Filter by property
    Given I am on the Sessions page
    When I set a filter value
    Then it calls GET /api/sessions with the filter parameter # UAC005
    And only matching Sessions are shown              # UAC006

  @SC04
  Scenario: Change page size above server max clamps to 500
    Given I change page size to 2000
    When the list reloads
    Then it calls GET /api/sessions?skip=0&take=500               # UAC007
    And at most 500 items are rendered                             # UAC008
