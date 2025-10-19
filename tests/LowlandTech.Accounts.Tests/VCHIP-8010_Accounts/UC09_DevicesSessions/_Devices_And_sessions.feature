
# --------------------------------------------------------------------
# Feature: Devices & sessions
# --------------------------------------------------------------------
@VCHIP-8010
@UC09
Feature: Devices & sessions
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - UNTRUST_DEVICE
    # ---------------------------------------------------------------
    @SC01
    Scenario: UNTRUST_DEVICE
        Given a trusted device
        When they untrust it
          Then Event DeviceUntrusted                                                            # UAC001
    # ---------------------------------------------------------------
    # SC02 - REVOKE_ALL_SESSIONS
    # ---------------------------------------------------------------
    @SC02
    Scenario: REVOKE_ALL_SESSIONS
        Given an authenticated account
        When they revoke all sessions
          Then Event SessionsRevoked                                                            # UAC002
