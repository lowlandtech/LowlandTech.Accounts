
# --------------------------------------------------------------------
# Feature: Addresses
# --------------------------------------------------------------------
@VCHIP-8010
@UC04
Feature: Addresses
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - UPDATE_ADDRESS
    # ---------------------------------------------------------------
    @SC01
    Scenario: UPDATE_ADDRESS
        Given an authenticated account with an address
        When they update it
          Then Event AddressUpdated                                                             # UAC001
    # ---------------------------------------------------------------
    # SC02 - REMOVE_ADDRESS
    # ---------------------------------------------------------------
    @SC02
    Scenario: REMOVE_ADDRESS
        Given an authenticated account
        When they remove an address
          Then Event AddressRemoved                                                             # UAC002
