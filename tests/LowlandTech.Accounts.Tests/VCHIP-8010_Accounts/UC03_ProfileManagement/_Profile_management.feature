
# --------------------------------------------------------------------
# Feature: Profile management
# --------------------------------------------------------------------
@VCHIP-8010
@UC03
Feature: Profile management
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - UPDATE_PROFILE
    # ---------------------------------------------------------------
    @SC01
    Scenario: UPDATE_PROFILE
        Given an authenticated account
        When they update profile fields
          Then Event ProfileUpdated                                                             # UAC001
