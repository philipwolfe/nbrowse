NBrowse: .NET assembly query utility
====================================

[![Build Status](https://travis-ci.org/r3c/nbrowse.svg?branch=master)](https://travis-ci.org/r3c/nbrowse)
[![license](https://img.shields.io/badge/license-mit-blue.svg)](https://opensource.org/licenses/MIT)

Overview
--------

NBrowse is a command-line utility to browse and execute search queries in .NET
compiled assemblies. It exposes loaded assemblies through a standard set of
traversable project/assembly/class/interface/method entities, and allows you
to run C# statements to query anything you want to retreive from them.

For example, this command line will find every class that implements interface
`IServer` from given assemblies:

    ./nbrowse.cli 'p => p.Assemblies \
        .SelectMany(a => a.Types) \
        .Where(t => t.Interfaces.Any(i => i.Name == "IServer"))' sample.dll

Usage
-----

Download latest [release](https://github.com/r3c/nbrowse/releases) from GitHub
or checkout code and build using your preferred
[.NET Core](https://dotnet.microsoft.com/download) or
[.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework-runtime/net472)
SDK.

Once you have a NBrowse.CLI executable file, run it with `-h` command line
argument to display help.

    ./nbrowse.cli -h

Licence
-------

This project is open-source, released under MIT licence. See `LICENSE.md` file
for details.

Author
------

[Rémi Caput](http://remi.caput.fr/) (github.com+cottle [at] mirari [dot] fr)
