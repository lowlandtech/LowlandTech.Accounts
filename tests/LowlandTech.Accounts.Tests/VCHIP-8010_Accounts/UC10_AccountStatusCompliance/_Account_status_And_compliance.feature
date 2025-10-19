
# --------------------------------------------------------------------
# Feature: Account status & compliance
# --------------------------------------------------------------------
@VCHIP-8010
@UC10
Feature: Account status & compliance
    Feature for VCHIP-8010


    # ---------------------------------------------------------------
    # SC01 - DEACTIVATE_REACTIVATE
    # ---------------------------------------------------------------
    @SC01
    Scenario: DEACTIVATE_REACTIVATE
        Given an active account
        When they deactivate then reactivate
          Then Event AccountDeactivated                                                         # UAC001
          Then Event AccountReactivated                                                         # UAC002
    # ---------------------------------------------------------------
    # SC02 - EXPORT_DELETE
    # ---------------------------------------------------------------
    @SC02
    Scenario: EXPORT_DELETE
        Given an authenticated account
        When they export then delete account
          Then Event AccountExportRequested                                                     # UAC003
          Then Event AccountDeleted                                                             # UAC004
