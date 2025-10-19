
# --------------------------------------------------------------------
# Feature: External logins
# --------------------------------------------------------------------
@VCHIP-8010
@UC06
Feature: External logins
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - UNLINK_EXTERNAL
    # ---------------------------------------------------------------
    @SC01
    Scenario: UNLINK_EXTERNAL
        Given an account with a linked Google login
        When they unlink it
          Then Event ExternalLoginUnlinked                                                      # UAC001
    # ---------------------------------------------------------------
    # SC02 - REFRESH_EXTERNAL
    # ---------------------------------------------------------------
    @SC02
    Scenario: REFRESH_EXTERNAL
        Given an expiring external login
        When the token is refreshed
          Then Event ExternalLoginRefreshed                                                     # UAC002
