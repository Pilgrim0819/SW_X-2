﻿using UnityEngine;

public class SquadBuilderConstants {

    public const string IMAGE_FOLDER_NAME = "images";
    public const string REBEL_ICON_IMAGE = "rebels";
    public const string EMPIRE_ICON_IMAGE = "imperials";
    public const string SCUM_ICON_IMAGE = "scum";
    public const string PREFABS_FOLDER_NAME = "Prefabs";
    public const string SHIP_NAME_PANEL = "ShipNamePanel";
    public const string PILOT_NAME_PANEL = "PilotNamePanel";
    public const string UPGRADE_IMAGE_HOLDER = "UpgradeImageHolder";
    public const string ACTION_ICON = "ActionIcon";
    public const string ACTION_SELECTOR = "ActionSelector";
    public const string SQUADRON_SHIP_HOLDER = "SquadronShipHolder";
    public const string SYSTEM_MESSAGE_PANEL = "SystemMessagePanel";
    public const string UPGRADE_SLOT = "UpgradeSlot";
    public const string UPGRADE_PANEL = "UpgradePanel";
    public const string SAVED_SQUADRON_PANEL = "SavedSquadronPanel";
    public const string FACTION_REBELS = "Rebels";
    public const string FACTION_EMPIRE = "Empire";
    public const string FACTION_SCUM = "Scum";

    public const float SHIP_PANEL_X_OFFSET = 0.0f;
    public const float SHIP_PANEL_Y_OFFSET = -15.0f;
    public const float SHIP_PANEL_Z_OFFSET = 0.0f;
    public const float PILOT_PANEL_X_OFFSET = 0.0f;
    public const float PILOT_PANEL_Y_OFFSET = -15.0f;
    public const float PILOT_PANEL_Z_OFFSET = 0.0f;
    public const float UPGRADE_IMAGE_X_OFFSET = 22.5f;
    public const float UPGRADE_IMAGE_Y_OFFSET = -24.5f;
    public const float UPGRADE_IMAGE_Z_OFFSET = 0.0f;
    public const float SQUADRON_SHIP_PANEL_X_OFFSET = 700.0f;
    public const float SQUADRON_SHIP_PANEL_Y_OFFSET = -80.0f;
    public const float SQUADRON_SHIP_PANEL_Z_OFFSET = 0.0f;
    public const float UPGRADE_SLOT_X_OFFSET = 550.0f;
    public const float UPGRADE_SLOT_Y_OFFSET = 0.0f;
    public const float UPGRADE_SLOT_Z_OFFSET = 0.0f;
    public const float UPGRADE_PANEL_X_OFFSET = 10.0f;
    public const float UPGRADE_PANEL_Y_OFFSET = -60.0f;
    public const float UPGRADE_PANEL_Z_OFFSET = 0.0f;
    public const float SAVED_SQUADRON_PANEL_X_OFFSET = 10.0f;
    public const float SAVED_SQUADRON_PANEL_Y_OFFSET = -40.0f;
    public const float SAVED_SQUADRON_PANEL_Z_OFFSET = 0.0f;

    private static Color PANEL_BACKGROUND_DEFAULT = new Color(0, 0, 0, 255);
    private static Color PANEL_BACKGROUND_HIGHLIGHT = new Color(255, 255, 255, 255);
    private static Color PANEL_BACKGROUND_SELECTED = new Color(76, 223, 255, 255);
    private static Color ADD_PILOT_BACKGROUND_DEFAULT = new Color(0, 255, 45, 255);

    public static Color getDefaultPanelBackground()
    {
        return PANEL_BACKGROUND_DEFAULT;
    }

    public static Color getHighlightPanelBackground()
    {
        return PANEL_BACKGROUND_HIGHLIGHT;
    }

    public static Color getSelectedPanelBackground()
    {
        return PANEL_BACKGROUND_SELECTED;
    }

    public static Color getDefaultAddPilotBackground()
    {
        return ADD_PILOT_BACKGROUND_DEFAULT;
    }
}
