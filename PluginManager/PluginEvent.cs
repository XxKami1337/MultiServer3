﻿namespace MultiServer.PluginManager
{
    public enum PluginEvent
    {
        TICK,
        DME_UDP_TICK,
        MEDIUS_PLAYER_ON_LOGGED_IN,
        MEDIUS_PLAYER_ON_LOGGED_OUT,
        MEDIUS_PLAYER_ON_CREATE_GAME,
        MEDIUS_PLAYER_ON_JOIN_GAME,
        MEDIUS_PLAYER_ON_JOINED_GAME,
        MEDIUS_PLAYER_ON_LEFT_GAME,
        MEDIUS_PLAYER_ON_CHAT_MESSAGE,
        MEDIUS_PLAYER_ON_GET_POLICY,
        MEDIUS_PLAYER_ON_GET_ANNOUNCEMENTS,
        MEDIUS_PLAYER_ON_GET_ALL_ANNOUNCEMENTS,
        MEDIUS_PLAYER_ON_WORLD_REPORT0,
        MEDIUS_GAME_ON_CREATED,
        MEDIUS_GAME_ON_DESTROYED,
        MEDIUS_GAME_ON_STARTED,
        MEDIUS_GAME_ON_ENDED,
        MEDIUS_GAME_ON_HOST_LEFT,
        MEDIUS_GAME_ON_PLAYER_JOIN_RESPONSE,

        MEDIUS_PLAYER_POST_WIDE_STATS,

        MEDIUS_ACCOUNT_LOGIN_REQUEST,
        MEDIUS_PRE_ACCOUNT_CREATE_ON_NOT_FOUND,
        MEDIUS_POST_ACCOUNT_CREATE_ON_NOT_FOUND,

        DME_PLAYER_ON_JOINED,
        DME_PLAYER_ON_LEFT,
        DME_GAME_ON_RECV_UDP,
        DME_GAME_ON_RECV_TCP,
    }
}