
# --------------------------------------------------------------------
# Feature: Email verification & password mgmt
# --------------------------------------------------------------------
@VCHIP-8010
@UC02
Feature: Email verification & password mgmt
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - SEND_VERIFY_EMAIL
    # ---------------------------------------------------------------
    @SC01
    Scenario: SEND_VERIFY_EMAIL
        Given an authenticated account
        When they request verification
          Then Event EmailVerificationSent                                                      # UAC001
          Then State EmailVerificationToken                                                     # UAC002
    # ---------------------------------------------------------------
    # SC02 - VERIFY_EMAIL
    # ---------------------------------------------------------------
    @SC02
    Scenario: VERIFY_EMAIL
        Given a pending verification token for the account
        When they verify email
          Then Event EmailVerified                                                              # UAC003
          Then State Account                                                                    # UAC004
          Then State EmailVerificationToken                                                     # UAC005
    # ---------------------------------------------------------------
    # SC03 - PASSWORD_RESET
    # ---------------------------------------------------------------
    @SC03
    Scenario: PASSWORD_RESET
        Given a user forgets password
        When they request a reset
          Then Event PasswordResetRequested                                                     # UAC003
          Then Event PasswordResetCompleted                                                     # UAC004
    # ---------------------------------------------------------------
    # SC04 - CHANGE_PASSWORD
    # ---------------------------------------------------------------
    @SC04
    Scenario: CHANGE_PASSWORD
        Given an authenticated account
        When they change password
          Then Event PasswordChanged                                                            # UAC005
