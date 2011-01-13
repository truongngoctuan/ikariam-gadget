////////////////////////////////////////////////////////////////////////////////
//
//  Ikariam Gadget Settings
//
////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////
//
// Load existing settings if available
//
////////////////////////////////////////////////////////////////////////////////
function LoadSettings() {
    frequency.selectedIndex = 0;
    System.Gadget.onSettingsClosing = SettingsClosing;

    if (System.Gadget.Settings.read("SettingExist") == true) {
        displayname.value = System.Gadget.Settings.read("displayname");
        username.value = System.Gadget.Settings.read("username");
        password.value = System.Gadget.Settings.read("password");
        frequency.selectedIndex = System.Gadget.Settings.read("frequency");
    }

    self.focus;
    document.body.focus();
}
////////////////////////////////////////////////////////////////////////////////
//
// Settings page closing handler
//
////////////////////////////////////////////////////////////////////////////////
function SettingsClosing(event) {
    if (event.closeAction == event.Action.commit) {
        System.Gadget.Settings.write("displayname", displayname.value);
        System.Gadget.Settings.write("username", username.value);
        System.Gadget.Settings.write("password", password.value);
        System.Gadget.Settings.write("frequency", frequency.selectedIndex);
        System.Gadget.Settings.write("SettingExist", true);
    }

    event.cancel = false;
}