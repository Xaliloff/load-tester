export interface TestConfigCreationRequest {
  name: string;
  url: string;
  headers: any[];
  body: string;
  concurrentUsers: number | null;
  newUsersCreationIntervalInSec: number | null;
  increaseUsersBy: number | null;
  durationInSeconds: number | null;
  requestDelayInMs: number | null;
}
