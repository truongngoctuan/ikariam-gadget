////////////////////////////////////////////////////////////////////////////////
//
// GMail reader javascript library 
//
////////////////////////////////////////////////////////////////////////////////
var OneSecond = 1000 * 60;  // Represents one second.
var frequency = 0;  // Feed refresh rate in minutes
var timeout;
var displayname = "";
var username = "";
var password = "";
var authenticated = false;
var isProcessing = false;

var context;
// Instance of the GadgetBuilder to load/unload .NET assemblies.  See GadgetInterop.js
var builder;
// .NET instance of the IkariamFramework.Gadget object.
var Framework;

////////////////////////////////////////////////////////////////////////////////
//
// State & GUI
//
////////////////////////////////////////////////////////////////////////////////
var States = { Welcome: 0, Overview: 1 }
function Context() {
    this.State = States.Welcome;
}

function GetWelcomeGUI(gadgetContent, gadgetBackgroundImage) {
    var content = "";
    gadgetContent.innerHTML = content;
    gadgetBackgroundImage.src = 'url(images/Welcome_undocked.png)';
    //gadgetBackgroundImage.src = 'url(images/animated_loading_gallery.gif)';
}
function GetOverviewGUI(gadgetContent, gadgetBackgroundImage) {
    var content = "";
    content += '<img id="town" class="overviewItem1" src="images/mayor.gif" onmousedown="javascript:onOverview(' + "'town'" + ');"/>';
    content += '<img id="military" class="overviewItem2" src="images/general.gif" onmousedown="javascript:onOverview(' + "'military'" + ');"/>';
    content += '<img id="research" class="overviewItem3" src="images/scientist.gif" onmousedown="javascript:onOverview(' + "'research'" + ');"/>';
    content += '<img id="diplomacy" class="overviewItem4" src="images/diplomat.gif" onmousedown="javascript:onOverview(' + "'diplomacy'" + ');"/>';
    content += '<img id="empire" class="overviewItem5" src="images/diplomat_active.gif" onmousedown="javascript:onOverview(' + "'empire'" + ');"/>';
    content += '<img id="event" class="overviewItem6" src="images/general_active.gif" onmousedown="javascript:onOverview(' + "'event'" + ');"/>';

    gadgetContent.innerHTML = content;
    gadgetBackgroundImage.src = 'url(images/Overview_undocked.png)';
}

function CreateGUI(oGadgetDocument) {
    var oGadgetContent = oGadgetDocument.getElementById("gadgetContent");
    var oGadgetBackgroundImage = oGadgetDocument.getElementById("backgroundImage");
    
    if (context.State == States.Welcome) {
        GetWelcomeGUI(oGadgetContent, oGadgetBackgroundImage);        
    }   
    else if (context.State == States.Overview) {
        GetOverviewGUI(oGadgetContent, oGadgetBackgroundImage);
    }
}

function SetState(state) {
    context.State = state;
    var oGadgetDocument = System.Gadget.document;
    if (context.State == States.Welcome) {
        oGadgetDocument.getElementById('state').innerHTML = "Welcome !!!";
    }    
    else if (context.State == States.Overview) {
        oGadgetDocument.getElementById('state').innerHTML = "Overview !!!";
    }
    else {
        oGadgetDocument.getElementById('state').innerHTML = "Unknown State !!!";
    }
    CreateGUI(oGadgetDocument);
}

function StateForward() {
    if (context.State == States.Welcome) {
        if(authenticated == false)
            return;
        SetState(States.Overview);
    } 
    else if (context.State == States.Overview) {
        return;
    }
    else {
        SetState(States.Welcome);
    }
}

function StateBackward() {
    if (context.State == States.Welcome) {
        return;
    }  
    else if (context.State == States.Overview) {
        SetState(States.Welcome);
    }
    else {
        SetState(States.Welcome);
    }
}

////////////////////////////////////////////////////////////////////////////////
//
// Set up the gadget and subscribe to events, Load/Unload assembly
//
////////////////////////////////////////////////////////////////////////////////
function LoadGadget() {
    System.Gadget.Flyout.file = "flyout.html";
    System.Gadget.Flyout.onShow = OnShowFlyout;
    System.Gadget.settingsUI = "settings.html";
    System.Gadget.onSettingsClosed = SettingsClosed;
    System.Gadget.onUndock = CheckDock;
    System.Gadget.onDock = CheckDock;
    // System.Gadget.visibilityChanged = VisibilityChanged;

    // Instance of the GadgetBuilder to load/unload .NET assemblies.  See GadgetInterop.js
    builder = new GadgetBuilder();
    // Initialize the adapter to call the .NET assembly
    builder.Initialize();

    // Load the IkariamFramework.dll assembly and create an instance of the IkariamFramework.Gadget type
    // .NET instance of the IkariamFramework.Gadget object.
    Framework = builder.LoadType(System.Gadget.path + "\\bin\\Debug\\IkariamFramework.dll", "IkariamFramework.Gadget");
    
    // Create GUI
    context = new Context();
    SetState(States.Welcome);
    // Resize based on dock state
    CheckDock();
    self.focus;
    document.body.focus();
}

function UnloadGadget() {
    builder.UnloadType(Framework);
    builder = null;
}
////////////////////////////////////////////////////////////////////////////////
//
// Call the Login method of the .NET object
// Return : @errorMessageCode
//              0  :   Success
//              >0 :   Fail
//
////////////////////////////////////////////////////////////////////////////////
function isProcessing() {
    return isProcessing;
}

function beginProcess() {
    if (isProcessing)
        return false;
    isProcessing = true;
    return true;
}

function endProcess() {
    isProcessing = false;
}

function Login() {
    authenticated = false;    
    var errorMessageCode = -1;    
    // Exit if no credentials exist
    if (displayname == "" || username == "" || password == "") {
        errorMessageCode = -1;
    }
    else {
        errorMessageCode = Framework.Login(username, password, /*"s5.vn.ikariam.com"*/ displayname);
    }
        
    if (errorMessageCode == 0) {
        // Success
        authenticated = true;
        SetState(States.Overview);              
    }
    else {
        // Fail
        SetState(States.Welcome);
    }
    return errorMessageCode;
}

function GetMessageError(errorCode) {
    return Framework.GetMessageError(errorCode);
}

function Actions() {
    this.Login = function() {
        if (!beginProcess())
            return -1;
        var errCode = Login();
        endProcess();

        return errCode;
    };
    this.GetMessageError = function(errCode) {
        if (!beginProcess())
            return -1;
        var errMessage = GetMessageError(errCode);
        endProcess();

        return errMessage;
    };
    /*this.Logins = function(count, current) {
        if (current == 0 && current < count) {
            if (!beginProcess())
                return;
        }
        Login(username, password);
        current++;
        if (current < count) {

            //setInterval("actions.Logins(" + count + ',' + current + ");", 1000);
            setTimeout(function() {
                actions.Logins(count, current);
            }, 2500);
        }
        else {
            endProcess();
        }
    };
    
    this.addTownToFlyoutContent = function(count, current, oFlyoutContent) {        
        if (current == 0 && current < count) {
            if (!beginProcess()) {
                oFlyoutContent.innerHTML = flyoutContent + '</table>';
                return;
            }
        }
        var town = Framework.EmpireOverviewUnit(current);
        flyoutContent += townToHTML(town, current%2);
        oFlyoutContent.innerHTML = flyoutContent + '</table>';
        current++;
        if (current < count) {
            setTimeout(function() {
                actions.addTownToFlyoutContent(count, current, oFlyoutContent);
            }, 500);
        }
        else {
            endProcess();
        }
    };*/
}
var actions = new Actions();

////////////////////////////////////////////////////////////////////////////////
//
// Read the saved settings
//
////////////////////////////////////////////////////////////////////////////////
function SettingsClosed(event) {
    if (event.closeAction == event.Action.commit) {
        if (System.Gadget.Settings.read("SettingExist") == true) {

            displayname = System.Gadget.Settings.read("displayname");
            username = System.Gadget.Settings.read("username");
            password = System.Gadget.Settings.read("password");

            var minutes = System.Gadget.Settings.read("frequency");
            if (minutes == 0) { minutes = 0.5; }
            frequency = (OneSecond * minutes);

            // Call Login since settings have been changed.
            actions.Login();                    
        }
        else {

        }
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
}
////////////////////////////////////////////////////////////////////////////////
//
// styles for gadget when undocked : resize
//
////////////////////////////////////////////////////////////////////////////////
function GadgetUndocked() {
    with (document.body.style)
        width = 400,
        height = 300;

    with (backgroundImage.style)
        width = 400,
        height = 300;

    gadgetGUI.className = "gadgetGUI_undocked";
}
////////////////////////////////////////////////////////////////////////////////
//
// styles for gadget when docked : resize
// 
////////////////////////////////////////////////////////////////////////////////
function GadgetDocked() {
    with (document.body.style)
        width = 130,
        height = 100;

    with (backgroundImage.style)
        width = 130,
        height = 100;

    gadgetGUI.className = "gadgetGUI_docked";
}

////////////////////////////////////////////////////////////////////////////////
//
// Overview : open Flyout to show overview
//
////////////////////////////////////////////////////////////////////////////////
var OverviewStates = { None: 0, Towns: 1, Military: 2, Research: 3, Diplomacy: 4, Empire : 5, Event : 6 }
var overviewState = OverviewStates.None;

function onOverview(controlId) {
    if (controlId == "town")
        showTowns();
    else if (controlId == "military")
        showMilitary();
    else if (controlId == "research")
        showResearch();
    else if (controlId == "diplomacy")
        showDiplomacy();
    else if (controlId == "empire")
        showEmpire();
    else if (controlId == "event")
        showEvent();
}

function showTowns() {
    overviewState = OverviewStates.Towns;
    ShowHideFlyout();
}
function showMilitary() {
    overviewState = OverviewStates.Military;
    ShowHideFlyout();
}
function showResearch() {
    overviewState = OverviewStates.Research;
    ShowHideFlyout();
}
function showDiplomacy() {
    overviewState = OverviewStates.Diplomacy;
    ShowHideFlyout();
}
function showEmpire() {
    overviewState = OverviewStates.Empire;
    ShowHideFlyout();
}
function showEvent() {
    overviewState = OverviewStates.Event;
    ShowHideFlyout();
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
// Display the list of infomation and details in the flyout window.  The code
// must be here and not in the ShowHideFlyout since the flyout page isn't
// available to find document elements until it's finished loading.
//
////////////////////////////////////////////////////////////////////////////////
function OnShowFlyout() {
    if (overviewState == OverviewStates.Towns) {
        SetTownOverviewGUI();
    }
    else if (overviewState == OverviewStates.Military) {
    }
    else if (overviewState == OverviewStates.Research) {
    }
    else if (overviewState == OverviewStates.Diplomacy) {
    }
    else if (overviewState == OverviewStates.Empire) {
        SetEmpireOverviewGUI();
    }
    else if (overviewState == OverviewStates.Event) {
    }
}

function SetTownOverviewGUI() {
    debugger;
    var oBackground = System.Gadget.Flyout.document.getElementById('flyoutBackgroundImage');
    var oFlyoutContent = System.Gadget.Flyout.document.getElementById('flyoutContent');
    oFlyoutContent.className = "townOverview";
    flyoutContent = "";
    flyoutContent += '<table>\
                <tr valign="bottom" style="background-image:url(images/empireOverview/button.gif)">\
	                <td name="Name"><div style="width:80px;"></div></td>\
	                <td name="Academy"><img src="images/townOverview/academy.gif" /></td>\
			        <td name="Barracks"><img src="images/townOverview/barracks.gif" /></td>\
			        <td name="Dump"><img src="images/townOverview/dump.gif" /></td>\
			        <td name="Embassy"><img src="images/townOverview/embassy.gif" /></td>\
			        <td name="Hideout"><img src="images/townOverview/hideout.gif" /></td>\
			        <td name="Museum"><img src="images/townOverview/museum.gif" /></td>\
			        <td name="Palace"><img src="images/townOverview/palace.gif" /></td>\
			        <td name="Shipyard"><img src="images/townOverview/shipyard.gif" /></td>\
			        <td name="Tavern"><img src="images/townOverview/tavern.gif" /></td>\
			        <td name="Temple"><img src="images/townOverview/temple.gif" /></td>\
			        <td name="Town hall"><img src="images/townOverview/townhall.gif" /></td>\
			        <td name="Town wall"><img src="images/townOverview/townwall.gif" /></td>\
			        <td name="Trading port"><img src="images/townOverview/tradingport.gif" /></td>\
			        <td name="Trading post"><img src="images/townOverview/tradingpost.gif" /></td>\
			        <td name="Warehouse"><img src="images/townOverview/warehouse.gif" /></td>\
			        <td name="Workshop"><img src="images/townOverview/workshop.gif" /></td>\
			        <td name="Architect\'s Office"><img src="images/townOverview/architect.gif" /></td>\
			        <td name="Carpenter"><img src="images/townOverview/carpenter.gif" /></td>\
			        <td name="Firework Test Area"><img src="images/townOverview/firework.gif" /></td>\
			        <td name="Optician"><img src="images/townOverview/optician.gif" /></td>\
			        <td name="Wine Press"><img src="images/townOverview/winepress.gif" /></td>\
			        <td name="Alchemist\'s Tower"><img src="images/townOverview/alchemist.gif" /></td>\
			        <td name="Forester\'s House"><img src="images/townOverview/forester.gif" /></td>\
			        <td name="Glassblower"><img src="images/townOverview/glassblower.gif" /></td>\
			        <td name="Stonemason"><img src="images/townOverview/stonemason.gif" /></td>\
			        <td name="Winegrower"><img src="images/townOverview/winegrower.gif" /></td>\
                </tr>';

    var townOverviewUnits = JSON.parse(Framework.GetTownOverviewUnits(), function(key, value) {
        var type;
        if (value && typeof value === 'object') {
            type = value.type;
            if (typeof type === 'string' && typeof window[type] === 'function') {
                return new (window[type])(value);
            }
        }
        return value;
    });

    var citiesCount = 0;
    for (var k in townOverviewUnits) {
        if (townOverviewUnits.hasOwnProperty(k))
            citiesCount++;
    }

    if (citiesCount > 0) {        
        var townUnit;
        for (var i = 0; i < citiesCount; i++) {
            //town = Framework.EmpireOverviewUnit(i);
            townUnit = townOverviewUnits[i];
            flyoutContent += townUnitToHTML(townUnit, i % 2);
        }
    }
    flyoutContent += '</table>';
    oFlyoutContent.innerHTML = flyoutContent;
}

function townUnitToHTML(townUnit, isOdd) {
	
    if (isOdd == 0)
	{
		var html = "";
		html += '<tr valign="bottom">';
		html += '<td><div style="width:80px;"><b>' + townUnit.TownName + '</b>' + '(' + townUnit.X + ',' + townUnit.Y + ')</div></td>';
		html += '<td>' + (townUnit.Buildings.Academy ? townUnit.Buildings.Academy.Lvl : '-') + '</td>';
		html += '<td>' + (townUnit.Buildings.Barracks ? townUnit.Buildings.Barracks.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Dump ? townUnit.Buildings.Dump.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Embassy ? townUnit.Buildings.Embassy.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Hideout ? townUnit.Buildings.Hideout.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Museum ? townUnit.Buildings.Museum.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Palace ? townUnit.Buildings.Palace.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Shipyard ? townUnit.Buildings.Shipyard.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Tavern ? townUnit.Buildings.Tavern.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Temple ? townUnit.Buildings.Temple.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Townhall ? townUnit.Buildings.Townhall.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Townwall ? townUnit.Buildings.Townwall.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.TradingPort ? townUnit.Buildings.TradingPort.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.TradingPost ? townUnit.Buildings.TradingPost.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Warehouse ? townUnit.Buildings.Warehouse.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Workshop ? townUnit.Buildings.Workshop.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Architect ? townUnit.Buildings.Architect.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Carpenter ? townUnit.Buildings.Carpenter.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Firework ? townUnit.Buildings.Firework.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Optician ? townUnit.Buildings.Optician.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.WinePress ? townUnit.Buildings.WinePress.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Alchemist ? townUnit.Buildings.Alchemist.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Forester ? townUnit.Buildings.Forester.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Glassblower ? townUnit.Buildings.Glassblower.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Stonemason ? townUnit.Buildings.Stonemason.Lvl : '-') + '</td>';
	    html += '<td>' + (townUnit.Buildings.Winegrower ? townUnit.Buildings.Winegrower.Lvl : '-') + '</td>';
        html += '</tr>';
		return html;
	}              
    else
	    return '<tr valign="bottom" style="background-color:#FDF7DD">\
	                        <td><div style="width:80px;"><b>' + townUnit.TownName + '</b>' + '(' + townUnit.X + ',' + townUnit.Y + ')</div></td>\
	                        <td>' + (townUnit.Buildings.Academy ? townUnit.Buildings.Academy.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Barracks ? townUnit.Buildings.Barracks.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Dump ? townUnit.Buildings.Dump.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Embassy ? townUnit.Buildings.Embassy.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Hideout ? townUnit.Buildings.Hideout.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Museum ? townUnit.Buildings.Museum.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Palace ? townUnit.Buildings.Palace.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Shipyard ? townUnit.Buildings.Shipyard.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Tavern ? townUnit.Buildings.Tavern.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Temple ? townUnit.Buildings.Temple.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Townhall ? townUnit.Buildings.Townhall.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Townwall ? townUnit.Buildings.Townwall.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.TradingPort ? townUnit.Buildings.TradingPort.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.TradingPost ? townUnit.Buildings.TradingPost.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Warehouse ? townUnit.Buildings.Warehouse.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Workshop ? townUnit.Buildings.Workshop.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Architect ? townUnit.Buildings.Architect.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Carpenter ? townUnit.Buildings.Carpenter.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Firework ? townUnit.Buildings.Firework.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Optician ? townUnit.Buildings.Optician.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.WinePress ? townUnit.Buildings.WinePress.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Alchemist ? townUnit.Buildings.Alchemist.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Forester ? townUnit.Buildings.Forester.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Glassblower ? townUnit.Buildings.Glassblower.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Stonemason ? townUnit.Buildings.Stonemason.Lvl : '-') + '</td>\
	                        <td>' + (townUnit.Buildings.Winegrower ? townUnit.Buildings.Winegrower.Lvl : '-') + '</td>\
                    </tr>';
}



function SetEmpireOverviewGUI() {
    debugger;
    var oBackground = System.Gadget.Flyout.document.getElementById('flyoutBackgroundImage');
    var oFlyoutContent = System.Gadget.Flyout.document.getElementById('flyoutContent');

    oFlyoutContent.className = "empireOverview";
    flyoutContent = "";
    flyoutContent += '<table>\
                <tr valign="bottom" style="background-image:url(images/empireOverview/button.gif)">\
	                <td name="Name"><div style="width:80px;"></div></td>\
	                <td name="ActionPoint"><img src="images/empireOverview/icon_action.gif" /></td>\
	                <td name="Population (PopulationLimit)"> <img src="images/empireOverview/icon_citizen.gif"/></td>\
	                <td name="Wood"><img src="images/empireOverview/icon_wood.gif" /></td>\
	                <td name="Wine"><img src="images/empireOverview/icon_wine.gif" /></td>\
	                <td name="Marble"><img src="images/empireOverview/icon_marble.gif" /></td>\
	                <td name="Crystal"><img src="images/empireOverview/icon_glass.gif" /></td>\
	                <td name="Sulphur"><img src="images/empireOverview/icon_sulfur.gif" /></td>\
	                <td name="ResearchPointPerHour"><img src="images/empireOverview/icon_research.gif" /></td>\
	                <td name="GoldPerHour"><img src="images/empireOverview/icon_gold.gif" /></td>\
                </tr>';

    //var citiesCount = Framework.GetEmpireOverviewUnitNum();
    
    var empireOverviewUnits = JSON.parse(Framework.GetEmpireOverviewUnits(), function(key, value) {
        var type;
        if (value && typeof value === 'object') {
            type = value.type;
            if (typeof type === 'string' && typeof window[type] === 'function') {
                return new (window[type])(value);
            }
        }
        return value;
    });
    
    var citiesCount = 0;
    for (var k in empireOverviewUnits) {
        if (empireOverviewUnits.hasOwnProperty(k))
            citiesCount++;
    }   
    
    if (citiesCount > 0) {
        var total = Framework.GetEmptyEmpireOverviewUnit();
        var empireUnit;
        for (var i = 0; i < citiesCount; i++) {
            //town = Framework.EmpireOverviewUnit(i);
            empireUnit = empireOverviewUnits[i];

            total.Population += empireUnit.Population;
            total.Wood += empireUnit.Wood;
            total.WoodPerHour += empireUnit.WoodPerHour;
            total.Wine += empireUnit.Wine;
            total.WinePerHour += empireUnit.WinePerHour;
            total.Marble += empireUnit.Marble;
            total.MarblePerHour += empireUnit.MarblePerHour;
            total.Crystal += empireUnit.Crystal;
            total.CrystalPerHour += empireUnit.CrystalPerHour;
            total.Sulphur += empireUnit.Sulphur;
            total.SulphurPerHour += empireUnit.SulphurPerHour;
            total.ResearchPointPerHour += empireUnit.ResearchPointPerHour;
            total.GoldPerHour += empireUnit.GoldPerHour;

            flyoutContent += empireUnitToHTML(empireUnit, i % 2);
        }
        
        flyoutContent += '<tr valign="bottom" style="background-color:#DDAE61">\
	                        <td><div style="width:80px;"><b>' + 'Total' + '</b></div></td>\
	                        <td>' + '-' + '</td>\
	                        <td>' + total.Population + '</td>\
	                        <td>' + total.Wood + ' (+' + total.WoodPerHour + ')</td>\
	                        <td>' + total.Wine + ' (+' + total.WinePerHour + ')</td>\
	                        <td>' + total.Marble + ' (+' + total.MarblePerHour + ')</td>\
	                        <td>' + total.Crystal + ' (+' + total.CrystalPerHour + ')</td>\
	                        <td>' + total.Sulphur + ' (+' + total.SulphurPerHour + ')</td>\
	                        <td>' + '+' + total.ResearchPointPerHour + '</td>\
	                        <td>' + '+' + total.GoldPerHour + '</td>\
                        </tr>';
    }
    flyoutContent += '</table>';
    oFlyoutContent.innerHTML = flyoutContent;
}

function empireUnitToHTML(empireUnit, isOdd) {
    if(isOdd == 0)
        return '<tr valign="bottom">\
	                        <td><div style="width:80px;"><b>' + empireUnit.TownName + '</b>' + '(' + empireUnit.X + ',' + empireUnit.Y + ')</div></td>\
	                        <td>' + empireUnit.ActionPoint + '</td>\
	                        <td>' + empireUnit.Population + '</td>\
	                        <td>' + empireUnit.Wood + ' (+' + empireUnit.WoodPerHour + ')</td>\
	                        <td>' + empireUnit.Wine + ' (+' + empireUnit.WinePerHour + ')</td>\
	                        <td>' + empireUnit.Marble + ' (+' + empireUnit.MarblePerHour + ')</td>\
	                        <td>' + empireUnit.Crystal + ' (+' + empireUnit.CrystalPerHour + ')</td>\
	                        <td>' + empireUnit.Sulphur + ' (+' + empireUnit.SulphurPerHour + ')</td>\
	                        <td>' + '+' + empireUnit.ResearchPointPerHour + '</td>\
	                        <td>' + '+' + empireUnit.GoldPerHour + '</td>\
                    </tr>';
    else
        return '<tr valign="bottom" style="background-color:#FDF7DD" >\
	                        <td><div style="width:80px;"><b>' + empireUnit.TownName + '</b>' + '(' + empireUnit.X + ',' + empireUnit.Y + ')</div></td>\
	                        <td>' + empireUnit.ActionPoint + '</td>\
	                        <td>' + empireUnit.Population + '</td>\
	                        <td>' + empireUnit.Wood + ' (+' + empireUnit.WoodPerHour + ')</td>\
	                        <td>' + empireUnit.Wine + ' (+' + empireUnit.WinePerHour + ')</td>\
	                        <td>' + empireUnit.Marble + ' (+' + empireUnit.MarblePerHour + ')</td>\
	                        <td>' + empireUnit.Crystal + ' (+' + empireUnit.CrystalPerHour + ')</td>\
	                        <td>' + empireUnit.Sulphur + ' (+' + empireUnit.SulphurPerHour + ')</td>\
	                        <td>' + '+' + empireUnit.ResearchPointPerHour + '</td>\
	                        <td>' + '+' + empireUnit.GoldPerHour + '</td>\
                    </tr>';                    
}
