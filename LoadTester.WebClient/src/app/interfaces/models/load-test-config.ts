import { LoadTest } from './load-test';

export interface LoadTestConfig {
  id: string;
  name: string;
  url: string;
  tests: LoadTest[];
  body: string;
  concurrentUsers: number;
  durationInSeconds: number;
  headers: [];
  increaseUsersBy: number;
  isDeleted: boolean;
  newUsersEveryXSec: number;
  requestDelay: number;
}
