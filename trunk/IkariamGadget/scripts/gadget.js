////////////////////////////////////////////////////////////////////////////////
//
// GMail reader javascript library 
//
////////////////////////////////////////////////////////////////////////////////
var OneSecond = 1000 * 60;  // Represents one second.
var frequency = 0;  // Feed refresh rate in minutes
var timeout;
var displayname = "";
var username = "";    // GMail email address
var password = "";  // GMail password
var authenticated = false;


// Instance of the GadgetBuilder to load/unload .NET assemblies.  See GadgetInterop.js
var builder = new GadgetBuilder();
// Initialize the adapter to call the .NET assembly
builder.Initialize();

// Load the IkariamFramework.dll assembly and create an instance of the IkariamFramework.Gadget type
// .NET instance of the IkariamFramework.Gadget object.
var Framework = builder.LoadType(System.Gadget.path + "\\bin\\Debug\\IkariamFramework.dll", "IkariamFramework.Gadget");

////////////////////////////////////////////////////////////////////////////////
//
// Set up the gadget and subscribe to events
//
////////////////////////////////////////////////////////////////////////////////
function LoadGadget() {
    //System.Gadget.Flyout.file = "Flyout.html";
    //System.Gadget.Flyout.onShow = OnShowFlyout;
    System.Gadget.settingsUI = "settings.html";
    System.Gadget.onSettingsClosed = SettingsClosed;
    System.Gadget.onUndock = CheckDock;
    System.Gadget.onDock = CheckDock;
    
    // Resize based on dock state
    CheckDock();

    self.focus;
    document.body.focus();
}
////////////////////////////////////////////////////////////////////////////////
//
// Call the GetUnreadMail method of the .NET object to read the user's 
// GMail account.  The number of items is displayed on top of the background
// image, and the image is changed based on whether there is new unread mail.
//
////////////////////////////////////////////////////////////////////////////////
function GetInfo() {
    
    // Exit if no credentials exist
    if (displayname == "" || username == "" || password == "") { return; }

    if (authenticated == false) {
        // Call this method every N seconds
        timeout = setTimeout('GetInfo()', frequency);

        debugger;
        var errorMessageCode = Framework.Login(username, password, "s15.en.ikariam.com");
        if (errorMessageCode == 0) {
            // Success
            authenticated = true;
            var displaynameGUI = document.getElementById('displayname');
            displaynameGUI.innerText = displayname;
            var errorGUI = document.getElementById('error');
            errorGUI.innerText = Framework.GetErrorMessage(errorMessageCode);
            var oldviewGUI = document.getElementById('oldview');
            oldviewGUI.innerText = Framework.btadvCities_Click();
        }
        else {
            // Fail
            authenticated = false;
            var displaynameGUI = document.getElementById('displayname');
            displaynameGUI.innerText = displayname;
            var errorGUI = document.getElementById('error');
            errorGUI.innerText = Framework.GetErrorMessage(errorMessageCode);
        }

        // Change the image based on the number of unread items
        //var background = (count > 0) ? 'images/gmailNew.png' : 'images/gmailNo.png';
        //backgroundImage.src = background;

        //SetLinkClass();
    }    
}
////////////////////////////////////////////////////////////////////////////////
//
// Shows or hides the flyout
//
////////////////////////////////////////////////////////////////////////////////
function ShowHideFlyout() {
    System.Gadget.Flyout.show = !System.Gadget.Flyout.show;
}
////////////////////////////////////////////////////////////////////////////////
//
// Display the list of unread mail and details in the flyout window.  The code
// must be here and not in the ShowHideFlyout since the flyout page isn't
// available to find document elements until it's finished loading.
//
////////////////////////////////////////////////////////////////////////////////
/*function OnShowFlyout() {
    var output = "";
    // Get the number of unread items
    var count = gateway.UnreadMailCount;

    // Loop through each mail item and build the HTML layout.
    for (var i = 0; i < count; i++) {
        var mailItem = gateway.GetMailItem(i);

        output += "<div id='mailHolder' class='mailHolder'>";
        output += "<div id='mailHeader' class='mailHeader' onclick='ShowHide(" + i + ")'>";
        output += mailItem.AuthorName + "&nbsp; &nbsp;" + mailItem.Title + "</div>";
        output += "<div id='mailBody" + i + "' class='mailBody'>";
        output += "<b>Subject:&nbsp;</b>" + mailItem.Title + "<br />";
        output += "<b>From:&nbsp;</b> " + mailItem.AuthorName + "&nbsp;[" + mailItem.AuthorEmail + "]<br /><br />";
        output += mailItem.Summary + "<br /><br />";
        output += "<a target='nw' href='" + mailItem.Link + "'>Click here to see the full message</a><br>";
        output += "</div></div>";
    }

    // Get the flyout document's element and add the HTML
    var mail = System.Gadget.Flyout.document.getElementById('mailOutput');

    // Clear the existing UI
    mail.innerHTML = "";
    // Update the UI with the current unread mail list
    mail.insertAdjacentHTML("beforeEnd", output);
}*/
////////////////////////////////////////////////////////////////////////////////
//
// Read the saved settings
//
////////////////////////////////////////////////////////////////////////////////
function SettingsClosed(event) {
    if (System.Gadget.Settings.read("SettingExist") == true) {

        displayname = System.Gadget.Settings.read("displayname");
        username = System.Gadget.Settings.read("username");
        password = System.Gadget.Settings.read("password");

        var minutes = System.Gadget.Settings.read("frequency");

        if (minutes == 0) { minutes = 0.5; }

        frequency = (OneSecond * minutes);

        //backgroundImage.src = 'images/gmailno.png';
        //config.style.visibility = 'hidden';

        // Call ReadMail since settings have been changed.  
        GetInfo();
    }
    else {
        //backgroundImage.src = 'images/gmailbw.png';
        //config.style.visibility = 'visible';
    }

    event.cancel = false;
}
////////////////////////////////////////////////////////////////////////////////
//
// Check if the gadget is docked or undocked.  Change images and CSS based on 
// the current docked state.
//
////////////////////////////////////////////////////////////////////////////////
function CheckDock() {
    if (!System.Gadget.docked) {
        GadgetUndocked();
    }
    else if (System.Gadget.docked) {
        GadgetDocked();
    }

    //SetLinkClass();
}
////////////////////////////////////////////////////////////////////////////////
//
// styles for gadget when undocked
//
////////////////////////////////////////////////////////////////////////////////
function GadgetUndocked() {
    with (document.body.style)
        width = 256,
        height = 256;

    with (backgroundImage.style)
        width = 256,
        height = 256;

    //config.className = 'configUndocked';
    //mailCountLink.style.fontSize = '30pt';
}
////////////////////////////////////////////////////////////////////////////////
//
// styles for gadget when docked
// 
////////////////////////////////////////////////////////////////////////////////
function GadgetDocked() {
    with (document.body.style)
        width = 130,
        height = 130;

    with (backgroundImage.style)
        width = 130,
        height = 130;

    //config.className = 'config';
    //mailCountLink.style.fontSize = '18pt';
}
////////////////////////////////////////////////////////////////////////////////
//
// Set the mail count link CSS class based on the number of unread items.  This
// keeps the numbers centered on the 'button' area of the image, and fixes
// what the CSS text-align: center doesn't do.
//
////////////////////////////////////////////////////////////////////////////////
/*function SetLinkClass() {
    var count = gateway.UnreadMailCount;
    var mailCnt = document.getElementById('mailCount');

    var linkClass = "";

    if (count < 10) // 0-9 unread items link css style
    {
        linkClass = 'ones';
    }
    else if (count < 100) // 10-99 unread items link css style
    {
        linkClass = 'tens';
    }
    else // // 100 or more unread items link css style
    {
        linkClass = 'hundreds';
    }

    if (!System.Gadget.docked) // Add "undocked" based on the gadget's docked state
    {
        linkClass += 'undocked';
    }

    mailCnt.className = linkClass;
}*/