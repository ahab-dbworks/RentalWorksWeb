#!/bin/bash
set -x #echo on


git subtree pull --prefix lib/Fw Fw develop --squash

read -n1 -r -p "Press any key to continue..." key