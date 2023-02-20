export interface LoadTest {
  id: string;
  name: string;
  startDate: Date;
  endTime: Date;
  status: string;
  runningSeconds: number;
  configurationId: string;
}
