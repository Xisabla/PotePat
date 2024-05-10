#!/usr/bin/env python3

# Utility python script to build and bundle a release for thunderstore

import json
import shutil
import subprocess
import xml.dom.minidom

CSPROJ_FILE = './PoteChanges.csproj'


def get_version(csproj_file):
    csproj = xml.dom.minidom.parse(csproj_file)

    return [
        group.getElementsByTagName('Version')[0]
        for group in csproj.getElementsByTagName('PropertyGroup')
        if group.getElementsByTagName('Version')
    ][0].childNodes[0].nodeValue


def synchronize_version(csproj_version):
    with open('./manifest.json', 'r', encoding='utf-8') as fp:
        manifest = json.load(fp)
        manifest_version = manifest["version_number"]

    if csproj_version is not manifest["version_number"]:
        manifest["version_number"] = csproj_version

        with open('./manifest.json', 'w', encoding='utf-8') as fp:
            json.dump(manifest, fp, indent=4)

    print(manifest_version, '-->', csproj_version)


def build():
    subprocess.run(["dotnet", "build", "--configuration", "Release"])


def create_archive(csjproj_version):
    shutil.make_archive(f'./release-{csjproj_version}', 'zip', './bin/Release/netstandard2.1')


if __name__ == '__main__':
    version = get_version(CSPROJ_FILE)

    synchronize_version(version)
    build()
    create_archive(version)
