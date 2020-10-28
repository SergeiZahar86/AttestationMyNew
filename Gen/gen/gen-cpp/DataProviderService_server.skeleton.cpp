// This autogenerated skeleton file illustrates how to build a server.
// You should copy it to another filename to avoid overwriting it.

#include "DataProviderService.h"
#include <thrift/protocol/TBinaryProtocol.h>
#include <thrift/server/TSimpleServer.h>
#include <thrift/transport/TServerSocket.h>
#include <thrift/transport/TBufferTransports.h>

using namespace ::apache::thrift;
using namespace ::apache::thrift::protocol;
using namespace ::apache::thrift::transport;
using namespace ::apache::thrift::server;

class DataProviderServiceHandler : virtual public DataProviderServiceIf {
 public:
  DataProviderServiceHandler() {
    // Your initialization goes here
  }

  void getCauses(std::vector<cause_t> & _return) {
    // Your implementation goes here
    printf("getCauses\n");
  }

  void getContractors(std::vector<contractor_t> & _return) {
    // Your implementation goes here
    printf("getContractors\n");
  }

  void getMat(std::vector<mat_t> & _return) {
    // Your implementation goes here
    printf("getMat\n");
  }

  void getPhoto(photo_t& _return, const std::string& part_id, const int car_id) {
    // Your implementation goes here
    printf("getPhoto\n");
  }

  void getPart(part_t& _return, const std::string& part_id) {
    // Your implementation goes here
    printf("getPart\n");
  }

  void getUser(std::string& _return, const std::string& login, const std::string& password, const std::string& empl_id) {
    // Your implementation goes here
    printf("getUser\n");
  }

  void getNum(std::string& _return, const std::string& part_id, const int car_id) {
    // Your implementation goes here
    printf("getNum\n");
  }

  void getOldPart(std::string& _return) {
    // Your implementation goes here
    printf("getOldPart\n");
  }

  bool setNum(const std::string& part_id, const int car_id, const std::string& num) {
    // Your implementation goes here
    printf("setNum\n");
  }

  bool setAtt(const std::string& part_id, const int car_id, const int att_code) {
    // Your implementation goes here
    printf("setAtt\n");
  }

  bool setUser(const std::string& part_id, const std::string& user) {
    // Your implementation goes here
    printf("setUser\n");
  }

  bool setTara(const std::string& part_id, const int car_id, const double tara) {
    // Your implementation goes here
    printf("setTara\n");
  }

  bool setZone(const std::string& part_id, const int car_id, const int zone) {
    // Your implementation goes here
    printf("setZone\n");
  }

  bool setCarry(const std::string& part_id, const int car_id, const double carry) {
    // Your implementation goes here
    printf("setCarry\n");
  }

  bool setShipper(const std::string& part_id, const int car_id, const int shipper) {
    // Your implementation goes here
    printf("setShipper\n");
  }

  bool setConsigner(const std::string& part_id, const int car_id, const int consigner) {
    // Your implementation goes here
    printf("setConsigner\n");
  }

  bool setMat(const std::string& part_id, const int car_id, const int mat) {
    // Your implementation goes here
    printf("setMat\n");
  }

  bool setCar(const std::string& part_id, const int car_id, const car_t& car) {
    // Your implementation goes here
    printf("setCar\n");
  }

  void startAtt(part_t& _return, const std::string& user) {
    // Your implementation goes here
    printf("startAtt\n");
  }

  bool endAtt(const std::string& part_id) {
    // Your implementation goes here
    printf("endAtt\n");
  }

  bool changePass(const std::string& login, const std::string& oldPass, const std::string& newPass, const std::string& newEmpl_id) {
    // Your implementation goes here
    printf("changePass\n");
  }

};

int main(int argc, char **argv) {
  int port = 9090;
  ::std::shared_ptr<DataProviderServiceHandler> handler(new DataProviderServiceHandler());
  ::std::shared_ptr<TProcessor> processor(new DataProviderServiceProcessor(handler));
  ::std::shared_ptr<TServerTransport> serverTransport(new TServerSocket(port));
  ::std::shared_ptr<TTransportFactory> transportFactory(new TBufferedTransportFactory());
  ::std::shared_ptr<TProtocolFactory> protocolFactory(new TBinaryProtocolFactory());

  TSimpleServer server(processor, serverTransport, transportFactory, protocolFactory);
  server.serve();
  return 0;
}

