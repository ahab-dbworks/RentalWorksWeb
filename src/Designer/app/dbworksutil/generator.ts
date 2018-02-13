namespace dbworksutil {

    export class generator {

        static number_id(): number {
            var d = new Date(),
                day = d.getDay(),
                year = d.getFullYear(),
                hour = d.getHours(),
                seconds = d.getMilliseconds(),
                rand = 0;
            return rand = day + year + hour + seconds;
        }   

    }

}