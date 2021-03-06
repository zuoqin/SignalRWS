﻿namespace ChatSample

open System
open Owin
open Microsoft.AspNet.SignalR
open Microsoft.Owin
open Dynamic
open System.Threading.Tasks

type Msg =
    {
        SentAt : string
        Name : string
        Message : string
    }

type ChatHub() =
    inherit Hub()
    override x.OnConnected() = 
        let t:Task = x.Clients.All?userConnected x.Context.QueryString.["user"]
        t.Wait()
        base.OnConnected()
    member x.Chat (msg : Msg) =
        let t:Task = x.Clients.All?broadcastMessage msg
        t.Wait()

type Startup() =
    member x.Configuration (app : IAppBuilder) = app.MapSignalR() |> ignore
