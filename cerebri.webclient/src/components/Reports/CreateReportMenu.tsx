import { useState } from "react";
import { formatDate, requestCreateReport } from "services";
import { IoMdCheckmark, IoMdClose } from "react-icons/io";
import { CreateReportRequest } from "data/dataTypes";

interface CreateReportMenuProps {
    setShowMenu: (value: boolean) => void;
};

const CreateReportMenu:React.FC<CreateReportMenuProps> = ({ setShowMenu }) => {
    const [startDate, setStartDate] = useState<Date | null>(new Date());
    const [endDate, setEndDate] = useState<Date | null>(new Date());

    const handleSubmit = async () => {
        const request: CreateReportRequest = {
            startDate: startDate,
            endDate: endDate
        };
        try {
            await requestCreateReport(request);
        } catch (err: any) {
            console.log(err.message || "Error creating report");
        }
        setShowMenu(false);
    };

    return(
        <div 
            className="fixed flex flex-col z-[1000] h-[175px] w-[400px] shadow-2xl rounded-lg bg-bittersweet
                       items-center justify-center gap-2"
        >
            <div className="flex flex-row gap-2 h-fit w-3/4 justify-between">
                <label className="font-semibold text-white">Start Date</label>
                <input
                    type="date"
                    value={formatDate(startDate)}
                    onChange={(e) => setStartDate(e.target.valueAsDate)}
                />
            </div>
            <div className="flex flex-row gap-2 h-fit w-3/4 justify-between">
                <label className="font-semibold text-white">End Date</label>
                <input
                    type="date"
                    value={formatDate(endDate)}
                    onChange={(e) => setEndDate(e.target.valueAsDate)}
                />
            </div>
            <div className="flex flex-row gap-2 mt-2">
                <IoMdCheckmark 
                    size={24} 
                    cursor={'pointer'}
                    onClick={handleSubmit}
                />
                <IoMdClose 
                    size={24} 
                    cursor={'pointer'}
                    onClick={() => setShowMenu(false)}
                />
            </div>
        </div>
    );
};

export default CreateReportMenu;