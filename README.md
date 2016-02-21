# WebbyTraffy

![Logo](https://raw.githubusercontent.com/dialex/WebbyTraffy/master/WebbyTraffy/Doc/Logo.png)

This WinForm application allows you to specify a list of URLs and **simulate web page requests**.

## Usage

![Screenshot](https://raw.githubusercontent.com/dialex/WebbyTraffy/master/WebbyTraffy/Doc/Screenshot.png)

There's a [ready to use executable](https://raw.githubusercontent.com/dialex/WebbyTraffy/master/WebbyTraffy/Doc/ReadyToUse/WebbyTraffy.exe) at `WebbyTraffy/Doc/ReadyToUse` folder. You can configure it to behave like a human visitor by:

- Using random HTTP headers to simulate browsers;
- Using random proxies to simulate countries;
- Staying on page for a random period of time to simulate human reading;

#### URLs & Proxies

Use the **Import...** buttons to load the target URLs and a list of proxies.

Check the `Proxies.txt` [file](https://raw.githubusercontent.com/dialex/WebbyTraffy/master/WebbyTraffy/Proxies.txt) to see an example. Proxies should be listed one per line following the syntax: `<CountryName> | <IP>:<PORT>`.

## Roadmap

- Migrate from WebBrowser to [Awesomium](http://www.codeproject.com/Tips/825526/Csharp-WebBrowser-vs-Gecko-vs-Awesomium-vs-OpenWeb).
- Use private proxies, the current list was obtained from public listings [here](http://proxylist.hidemyass.com/) and [here](https://incloak.com/proxy-list).

## License

MIT License Copyright (c) 2016 Diogo Nunes
