#!/bin/bash
set -x #echo on

#Show the differences between the local subtree and the remote
git diff Fw/develop develop:lib/Fw

read -n1 -r -p "Press any key to continue..." key