
# --------------------------------------------------------------------
# Feature: Two-factor & recovery codes
# --------------------------------------------------------------------
@VCHIP-8010
@UC08
Feature: Two-factor & recovery codes
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - GENERATE_CODES
    # ---------------------------------------------------------------
    @SC01
    Scenario: GENERATE_CODES
        Given 2FA is enabled
        When they generate codes
          Then Event RecoveryCodesGenerated                                                     # UAC001
    # ---------------------------------------------------------------
    # SC02 - ROTATE_CODES
    # ---------------------------------------------------------------
    @SC02
    Scenario: ROTATE_CODES
        Given existing codes
        When they rotate codes
          Then Event RecoveryCodesRotated                                                       # UAC002
