
export interface QueueConfiguration {
  serverUrl: string;
  queueName: string;
  exchange: string;
  username: string;
  password: string;
  virtualHost: string;
  port: number;
  prefetchSize: number;
  prefetchCount: number;
  heartbeat: number;
}
