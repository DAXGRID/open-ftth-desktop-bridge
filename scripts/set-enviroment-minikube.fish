# Kafka
export KAFKA__SERVER=(minikube ip):(kubectl describe service openftth-kafka-cluster-kafka-external-bootstrap -n openftth | grep NodePort | grep -o '[0-9]\+')
export KAFKA__POSITIONFILEPATH="/tmp/"
export KAFKA__EVENTGEOGRAPHICALAREAUPDATEDCONSUMER="event-geographical-area-updated-desktop-bridge-consumer"
export KAFKA__EVENTGEOGRAPHICALAREAUPDATED="event.geographical-area-updated"

# Logging
export SERILOG__MINIMUMLEVEL="Information"
