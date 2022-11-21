import { Input } from "reactstrap"

const SearchBar = (props) => {
  return (
    <div className="container-fluid pt-4 px-4">
      <div className="row g-4">
        <div className ="col-sm-12 col-md-6 col-xl-4 mx-auto">
          <div className="bg-secondary text-center rounded p-4">
            <form className="d-none d-md-flex ms-4 m-auto"> 
              <i className="fa fa-search" style={{ marginTop: "11px", marginRight: "8px", cursor: "pointer" }}/>
              <Input id="searchBar" type="search" {...props} />
            </form>
          </div>
        </div>
      </div>
    </div>
  )
} 

export default SearchBar;