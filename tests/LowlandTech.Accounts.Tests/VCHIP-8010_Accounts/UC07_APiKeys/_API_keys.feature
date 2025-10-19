
# --------------------------------------------------------------------
# Feature: API keys
# --------------------------------------------------------------------
@VCHIP-8010
@UC07
Feature: API keys
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - ROTATE_APIKEY
    # ---------------------------------------------------------------
    @SC01
    Scenario: ROTATE_APIKEY
        Given an existing API key
        When they rotate it
          Then Event ApiKeyRotated                                                              # UAC001
    # ---------------------------------------------------------------
    # SC02 - DISABLE_APIKEY
    # ---------------------------------------------------------------
    @SC02
    Scenario: DISABLE_APIKEY
        Given a compromised API key
        When they disable it
          Then Event ApiKeyDisabled                                                             # UAC002
