import {CheckInsTable, ConfirmationPopUp, CreateCheckIn, ErrorPopUp} from "..";
import { useCheckIns } from "hooks";
import { useState } from "react";

const CheckIns = () => {
    const {checkIns, deleteCheckIn, error, setError } = useCheckIns();
    const [showCreateMenu, setShowCreateMenu] = useState<boolean>(false);
    const [showConfirmPopUp, setShowConfirmPopUp] = useState<boolean>(false);
    const [actionToConfirm, setActionToConfirm] = useState<() => void>(() => () => {});

    const handleDelete = (checkInId: string) => {
        setActionToConfirm(() => () => deleteCheckIn(checkInId));
        setShowConfirmPopUp(true);
    };

    const handleConfirm = () => {
        actionToConfirm();
        setShowConfirmPopUp(false);
    };

    return(
        <div className="h-full w-full flex flex-col justify-center items-center bg-pearlLusta">
            <h1 className="text-2xl font-bold">Check-Ins</h1>
            <CheckInsTable 
                checkIns={checkIns}
                onDelete={handleDelete}
                showCreate={setShowCreateMenu}
            />
            {showCreateMenu && <CreateCheckIn setShowOverlay={setShowCreateMenu} />}
            {showConfirmPopUp && (
                <ConfirmationPopUp
                    message="Are you sure you want to delete this CheckIn?"
                    onConfirm={handleConfirm}
                    onCancel={() => setShowConfirmPopUp(false)}
                />
            )}
            {error && <ErrorPopUp message={error} setError={setError} />}
        </div>
    );
};

export default CheckIns;