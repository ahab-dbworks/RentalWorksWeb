#!/bin/bash
set -x #echo on


git subtree push --prefix=lib/Fw Fw develop

read -n1 -r -p "Press any key to continue..." key