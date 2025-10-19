
# --------------------------------------------------------------------
# Feature: Preferences
# --------------------------------------------------------------------
@VCHIP-8010
@UC05
Feature: Preferences
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - SET_PREFERENCE
    # ---------------------------------------------------------------
    @SC01
    Scenario: SET_PREFERENCE
        Given an authenticated account
        When they set a preference
          Then Event PreferenceSet                                                              # UAC001
    # ---------------------------------------------------------------
    # SC02 - REMOVE_PREFERENCE
    # ---------------------------------------------------------------
    @SC02
    Scenario: REMOVE_PREFERENCE
        Given an authenticated account
        When they remove a preference
          Then Event PreferenceRemoved                                                          # UAC002
