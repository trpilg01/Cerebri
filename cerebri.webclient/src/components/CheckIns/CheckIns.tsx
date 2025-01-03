import { CheckIn, Mood } from "data/dataTypes";
import { useEffect, useState } from "react";
import { FaTrash } from "react-icons/fa";
import { getColor, getDateString, requestCheckIns, requestDeleteCheckIn } from "services";

const CheckIns = () => {
    const [checkIns, setCheckIns] = useState<CheckIn[] | undefined>(undefined);

    const handleDelete = async (checkInId: string) => {
        await requestDeleteCheckIn(checkInId);
        const updatedCheckIns = checkIns?.filter((item) => (
            item.id !== checkInId
        ));
        setCheckIns(updatedCheckIns);
    };

    useEffect(() => {
        const fetchCheckIns = async () => {
            const response = await requestCheckIns();
            setCheckIns(response);
            console.log(response);
        };

        fetchCheckIns();
    }, []);

    return(
<div className="h-full w-full flex flex-col justify-center items-center bg-pearlLusta">
            <h1 className="text-2xl font-bold">Journals</h1>
            <div className="h-5/6 w-5/6 mt-10 overflow-y-scroll scrollbar-none">
                <table className="w-full bg-blizzardBlue rounded-md shadow-xl">
                    <thead>
                        <th className="px-6 py-3 text-center text-black uppercase tracking-wider">Date</th>
                        <th className="px-6 py-3 text-center text-black uppercase tracking-wider">Moods</th>
                        <th className="px-6 py-3 text-center text-black uppercase tracking-wider">Notes</th>

                    </thead>
                    <tbody>
                        {checkIns?.map((item) => (
                            <tr>
                                <td className="text-center">{getDateString(item.createdAt)}</td>
                                <td className="text-center min-w-32">
                                    {item.moods.map((mood) => (
                                        <button
                                            key={mood.id}
                                            className={`h-fit p-1 text-xs mx-2 rounded-lg bg-bittersweet ${getColor(mood.type)} shadow-md`}
                                        >
                                            {mood.name}
                                        </button>
                                    ))}
                                </td>
                                <td className="flex justify-center tracking-wider max-h-40 overflow-y-scroll scrollbar-none p-2">
                                    <div className="text-center">
                                        {item.content}
                                    </div>
                                </td>
                                <td className="justify-center min-w-10 items-center">
                                    <FaTrash cursor={'pointer'} onClick={() => handleDelete(item.id)}/>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CheckIns;