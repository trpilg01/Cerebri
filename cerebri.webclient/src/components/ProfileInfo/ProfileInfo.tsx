import { UserInfo } from "data/dataTypes";
import { useEffect, useState } from "react";
import { requestUpdateUserInfo, requestUserInfo } from "services";
import { IoMdCheckmark } from "react-icons/io";
import { MdEdit } from "react-icons/md";

const ProfileInfo = () => {
    const [isEditMode, setIsEditMode] = useState<boolean>(false);
    const [email, setEmail] = useState<string>('');
    const [firstName, setFirstName] = useState<string>('');
    const [reportFrequency, setReportFrequency] = useState<string>('Weekly');
    const [error, setError] = useState<string | null>(null);

    const handleSave = async () => {
        if (email.length === 0 || email.length <= 5) {
            setError("Invalid email length");
            return;
        }
        
        const request: UserInfo = {
            email: email,
            firstName: firstName
        };

        try {
            setError(null);
            await requestUpdateUserInfo(request);            
        } catch (err: any) {
            setError(err.message || "Error updating user");
        }
        setIsEditMode(false);
    }

    useEffect(() => {
        const fetchUserInfo = async () => {
            try {
                setError(null);
                const userInfo: UserInfo | undefined = await requestUserInfo();
                if (typeof(userInfo) === 'undefined'){
                    setError("Error fetching user information");
                    return;
                }
                setEmail(userInfo.email);
                setFirstName(userInfo.firstName);         
            } catch (err: any) {
                setError(err.message || "Error updating user");
            }
        }

        fetchUserInfo();
    }, []);

    return (
        <div className="flex flex-col h-full w-full bg-pearlLusta justify-center items-center">
            <div className="h-1/3 w-1/3 flex flex-col bg-blizzardBlue rounded-md justify-center items-center">
                <div className="flex flex-col h-fit w-3/4 gap-1 p-2">
                    {error ?? <a>{error}</a>}
                    <div className="flex flex-row justify-between">
                        <div className="h-fit w-1/2">
                            <a className="font-semibold">Email:</a>
                        </div>
                        {isEditMode ?
                            <div className="h-fit w-1/2 items-center text-center">
                                <input 
                                    type="text" 
                                    value={email} 
                                    onChange={(e) => setEmail(e.target.value)} 
                                    className="h-full w-full text-center border-none bg-white rounded-md"
                                    maxLength={50}
                                />
                            </div>
                            : <div className="h-fit w-1/2 text-center"><a>{email}</a></div>
                        }
                    </div>

                    <div className="flex flex-row justify-between items-center">
                        <div className="h-fit w-1/2">
                            <a className="font-semibold">First Name:</a>
                        </div>
                        {isEditMode ?
                            <div className="h-fit w-1/2 items-center">
                                <input 
                                    type="text" 
                                    value={firstName} 
                                    onChange={(e) => setFirstName(e.target.value)} 
                                    maxLength={50}
                                    className=" h-full w-full text-center border-none bg-transparent bg-white rounded-md"
                                />
                            </div>
                            : <div className="h-fit w-1/2 text-center"><a>{firstName}</a></div>
                        }
                    </div>
                    <div className="flex flex-row justify-between">
                        <label className="font-semibold">Report Frequency</label>
                        {isEditMode ?
                            <div className="h-fit w-1/2 items-center">
                                <input 
                                    type="text" 
                                    value={reportFrequency} 
                                    onChange={(e) => setReportFrequency(e.target.value)} 
                                    maxLength={50}
                                    className=" h-full w-full text-center border-none bg-white rounded-md"
                                />
                            </div>
                            : <div className="h-fit w-1/2 text-center"><a>{reportFrequency}</a></div>
                        }
                    </div>
                </div>
                <div className="flex flex-row justify-center items-center">
                    {isEditMode ? 
                        <IoMdCheckmark size={20} cursor={'pointer'} onClick={handleSave}/>
                        : <MdEdit size={20} cursor={'pointer'} onClick={() => setIsEditMode(true)}/> 
                    }
                </div>
            </div>
        </div>
    );
};

export default ProfileInfo;