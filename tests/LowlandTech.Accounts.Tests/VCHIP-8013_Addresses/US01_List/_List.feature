
# --------------------------------------------------------------------
# Feature: Addresses - List and filter
# --------------------------------------------------------------------
@VCHIP-8013 @US01 @Blazor @API @CRUD
Feature: List and filter Addresses
  As a user
  I want to browse Addresses with paging and filters
  So that I can quickly find and manage Addresses

  @SC01
  Scenario: Load first page with default sorting
    Given I open the Addresses page
    When the page loads the first page with default size
    Then it calls GET /api/addresses?skip=0&take=100               # UAC001
    And the table renders the returned Addresses                   # UAC002

  @SC02
  Scenario: Search by Id or Name
    Given I am on the Addresses page with data available
    When I enter "search-term" in the search box and apply
    Then it calls GET /api/addresses?skip=0&take=100&search=search-term # UAC003
    And only Addresses whose Id or Name contains "search-term" are shown # UAC004

  @SC03
  Scenario: Filter by property
    Given I am on the Addresses page
    When I set a filter value
    Then it calls GET /api/addresses with the filter parameter # UAC005
    And only matching Addresses are shown              # UAC006

  @SC04
  Scenario: Change page size above server max clamps to 500
    Given I change page size to 2000
    When the list reloads
    Then it calls GET /api/addresses?skip=0&take=500               # UAC007
    And at most 500 items are rendered                             # UAC008
