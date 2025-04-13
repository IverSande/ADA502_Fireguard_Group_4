import grpc
import TestService_pb2
import TestService_pb2_grpc

def run():
    # Connect to gRPC server (update the port if needed)
    channel = grpc.insecure_channel("host.docker.internal:5001")
    stub = TestService_pb2_grpc.TestServiceStub(channel)

    # Create request
    request = TestService_pb2.DataRequest(data_id="1")

    # Call RPC
    response = stub.SendData(request)
    print(f"Temperature: {response.temperature}")

if __name__ == "__main__":
    run()
