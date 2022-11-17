import { useSelector } from "react-redux";
import { uploadListSelector } from "../../../state/slices/UploadListSlice";
import { UploadListItem } from "./UploadListItem";

export const UploadList = (props) => {
    const uploadList = useSelector(uploadListSelector);

    return (
        <div className="container-fluid pt-4 px-4">
            <div className="row g-4">
                {uploadList.map((item, index) => <UploadListItem item={item} key={index} shops={props.shops}/>)}
            </div>
        </div>
    );
}