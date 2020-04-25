import { Time } from '@angular/common';

export interface Day {
    reportedDate: Date;
    userId: number;
    daySequenceId: number;
    projectId: number;
    startTime: Time;
    endTime: Time;
    remark: string;
}