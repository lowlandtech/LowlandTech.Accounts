
# --------------------------------------------------------------------
# Feature: Account lifecycle
# --------------------------------------------------------------------
@VCHIP-8010
@UC01
Feature: Account lifecycle
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - REGISTER_ACCOUNT
    # ---------------------------------------------------------------
    @SC01
    Scenario: REGISTER_ACCOUNT
        Given a new user with email/password
        When they register
          Then Event AccountRegistered                                                          # UAC001
    # ---------------------------------------------------------------
    # SC02 - LOGIN
    # ---------------------------------------------------------------
    @SC02
    Scenario: LOGIN
        Given an existing registered account
        When they login
          Then Event LoggedIn                                                                   # UAC002
    # ---------------------------------------------------------------
    # SC03 - LINK_EXTERNAL
    # ---------------------------------------------------------------
    @SC03
    Scenario: LINK_EXTERNAL
        Given an existing account
        When they link a Google login
          Then Event ExternalLoginLinked                                                        # UAC003
    # ---------------------------------------------------------------
    # SC04 - MANAGE_PROFILE
    # ---------------------------------------------------------------
    @SC04
    Scenario: MANAGE_PROFILE
        Given an authenticated account
        When they update profile fields
          Then Event ProfileUpdated                                                             # UAC004
    # ---------------------------------------------------------------
    # SC05 - ADD_ADDRESS
    # ---------------------------------------------------------------
    @SC05
    Scenario: ADD_ADDRESS
        Given an authenticated account
        When they add a shipping address
          Then Event AddressAdded                                                               # UAC005
    # ---------------------------------------------------------------
    # SC06 - API_KEYS
    # ---------------------------------------------------------------
    @SC06
    Scenario: API_KEYS
        Given an authenticated account
        When they generate an API key
          Then Event ApiKeyCreated                                                              # UAC006
    # ---------------------------------------------------------------
    # SC07 - SECURITY_2FA
    # ---------------------------------------------------------------
    @SC07
    Scenario: SECURITY_2FA
        Given an authenticated account
        When they enable two-factor
          Then Event TwoFactorEnabled                                                           # UAC007
