#!/usr/bin/env bash

# Kafka
export KAFKA__SERVER=$(minikube ip):$(kubectl describe service openftth-kafka-cluster-kafka-external-bootstrap -n openftth | grep NodePort | grep -o '[0-9]\+')
export KAFKA__NOTIFICATIONGEOGRAPHICALAREAUPDATEDCONSUMER="notification-geographical-area-updated-desktop-bridge-consumer"
export KAFKA__NOTIFICATIONGEOGRAPHICALAREAUPDATED="notification.geographical-area-updated"

# LoggingDebug
export SERILOG__MINIMUMLEVEL="Debug"
