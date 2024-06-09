## Purpose
Where all the components and funcionalities of the frontend are.
If you need to use one of them in another component then it must be refactored and placed in shared/components.

### Children

- [abilities](): Section where all information related to abilities (STR, DEX,...) is and and where can be modified
- [account](): Section where user can manage other sheets, account, change language (only mobile) and more.
- [death-saves](): Section where all information related to death saves is (Failures, Sucesses, ...) and where can be modified
- [home](): The first view that appear when a user enters the web.
- [items](): Section where all information related to items (Weapons, armors, shields, ...) is and where can be modified (add, remove, equip,....)
- [lore](): Section where all lore about the character can be found (Background history, allies, enemies,...)
- [mobile-slider](): Utility component used to handle all the logic related to the mobile slider.
- [pc-slider](): Utility component used to handle all the logic related to the pc slider.
- [save-button](): Utility component used to handle everything related to the save button in desktop version.
- [spellcasting](): Section where all the information related to Spellcasting can be found (User won't be able to modify it because will be automatically filled)
- [spells](): Section where all the information about spells (list of spells, search, prepare,...) can be found and modified by user
- [wallet](): Section where all the information about the money of the character can be found and where user can add and remove coins.
