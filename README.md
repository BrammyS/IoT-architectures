<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">IoT-architectures Project</h3>

  <p align="center">
    A prototype for the IoT-architectures class during the IoT specialization at Saxion University of Applied Sciences.
    <br />
    <a href="https://github.com/BrammyS/IoT-architectures/issues">Report Bug</a>
    ·
    <a href="https://github.com/BrammyS/IoT-architectures/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project
A project to create a GPS tracker with a web-assbemly dashboard to see the temperature of specific GPS locations.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


### Built With

This project is built with the following technologies:
* [.NET 7.0](https://dotnet.microsoft.com/)
* [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
* [ASP.NET Core Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
* [FyPi](https://docs.pycom.io/datasheets/development/fipy/)
* [PyTrack shield](https://docs.pycom.io/datasheets/expansionboards/pytrack/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

* [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Your favorite C# IDE. Rider, Visual Studio, Visual Studio Code, etc.
* Visual Studio Code or Atom for Pymakr, see [this installation guide for more info](https://docs.pycom.io/gettingstarted/).


### Installation

1. Get your LoRa device keys at [https://portal.kpnthings.com](https://portal.kpnthings.com)
2. Clone the repo
   ```sh
   git clone https://github.com/BrammyS/IoT-architectures.git
   ```
3. Create a `env.py` file from the [`env.example.py`](https://github.com/BrammyS/IoT-architectures/blob/main/src/firmware/env.template.py) file and fill in your device keys
4. Set the correct secrets for the API. See [Secrets.md](https://github.com/BrammyS/IoT-architectures/blob/main/Solution%20items/Secrets.md) for the full commands.
5. Create a [cloudflare tunnel](https://developers.cloudflare.com/cloudflare-one/connections/connect-networks/get-started/)
6. Make a `docker.compose` with the correct tunnel secret from the [`template.docker.compose`](https://github.com/BrammyS/IoT-architectures/blob/main/Solution%20items/template.docker-compose.yaml) file.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->
## Usage

Create a flow with your own device and webhook on the [KPN Things Portal](https://portal.kpnthings.com).
![](https://cdn.brammys.com/2023/10/A1m2OzEYHnecXrUg5e4Dr5JabwhfbOJvx8GYgF4NdvRKjlt9kEvquJalQAJAcVDs.png)

Run the docker compose file:
```sh
docker-compose up
```

Run the FyPi with the PyTrack shield. A green light should show up after 5-10 seconds.
This means the device is connected to the LoRa.
The following LEDs mean the following:
* **Purple**: Connecting to LoRa
* **Green**: Connected to LoRa, but does not have GPS yet
* **Blue**: Has GPS, but is not connected to LoRa
* **White**: Connected to LoRa and has GPS
* **Red**: Something is horribly wrong! LoRa is not connected and GPS is not working.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Readme template](https://github.com/othneildrew/Best-README-Template)
* [Font Awesome](https://fontawesome.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/BrammyS/IoT-architectures.svg?style=for-the-badge
[contributors-url]: https://github.com/BrammyS/IoT-architectures/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/BrammyS/IoT-architectures.svg?style=for-the-badge
[forks-url]: https://github.com/BrammyS/IoT-architectures/network/members
[stars-shield]: https://img.shields.io/github/stars/BrammyS/IoT-architectures.svg?style=for-the-badge
[stars-url]: https://github.com/BrammyS/IoT-architectures/stargazers
[issues-shield]: https://img.shields.io/github/issues/BrammyS/IoT-architectures.svg?style=for-the-badge
[issues-url]: https://github.com/BrammyS/IoT-architectures/issues
[license-shield]: https://img.shields.io/github/license/BrammyS/IoT-architectures.svg?style=for-the-badge
[license-url]: https://github.com/BrammyS/IoT-architectures/blob/master/LICENSE.txt
