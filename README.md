<p>
    <a href="https://vaunt.dev">
        <img src="https://api.vaunt.dev/v1/github/entities/{{github_username}}/contributions?format=svg" width="350" title="Includes public contributions"/>
    </a>
</p>

# Desktop Facebook Application

The goal of this project is to provide access to Facebook in a way that is useful for someone using an eye gaze device. The application is built using Facebook's Graph API to request the necessary information from Facebook. It is built in C#, using the WPF format(as opposed to WinForms), and is being constructed in the Visual Studio IDE.

## Getting Started

To run this program using your own Facebook account, go to the [Graph API Explorer](https://developers.facebook.com/tools/explorer/) and request an Access Token. Open the MainWindow.xaml.cs file and paste the token into the "new FacebookClient" function. The program will then only have permission for what it was given when the token was requested.

## Built with

* [Graph API](https://developers.facebook.com/docs/graph-api/)
* [Visual Studio](https://www.visualstudio.com/)

## Authors
* Jacob Bechler
* Rory Hector
* Nick Charles

