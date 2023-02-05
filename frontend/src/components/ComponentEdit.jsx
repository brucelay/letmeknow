import FetchModule from "./modules/FetchModule";

export default function ComponentEdit(props) {
  return (
    <For each={props.functions}>
      {(func, i) => {
        return (
          <div>
            {() => {
              switch (func.function) {
                case "fetch":
                  return (
                    <FetchModule
                      index={i}
                      setFunctions={props.setFunctions}
                    />
                  );
              }
            }}
          </div>
        );
      }}
    </For>
  );
}
